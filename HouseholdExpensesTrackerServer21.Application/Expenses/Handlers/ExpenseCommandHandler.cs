using HouseholdExpensesTrackerServer21.Application.Expenses.Commands;
using HouseholdExpensesTrackerServer21.Common.Command;
using HouseholdExpensesTrackerServer21.Common.Type;
using HouseholdExpensesTrackerServer21.Domain.Expenses.Models;
using HouseholdExpensesTrackerServer21.Domain.Expenses.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HouseholdExpensesTrackerServer21.Application.Expenses.Handlers
{
    public class ExpenseCommandHandler : ICommandHandlerAsync<CreateExpenseCommand>,
                                         ICommandHandlerAsync<ModifyExpenseCommand>
    {
        private readonly IExpenseRepository _expenses;

        public ExpenseCommandHandler(IExpenseRepository expenses)
        {
            _expenses = expenses;
        }

        public async Task HandleAsync(CreateExpenseCommand message, CancellationToken token = default(CancellationToken))
        {
            var expense = Expense.Create(message.ExpenseId, message.HouseholdId, message.ExpenseTypeId, message.Name,
                message.Description, message.Amount, message.Date, Period.Create(message.PeriodStart, message.PeriodEnd));
            _expenses.Add(expense);
            await _expenses.SaveChangesAsync(token);
        }

        public async Task HandleAsync(ModifyExpenseCommand message, CancellationToken token = default(CancellationToken))
        {
            var expense = await _expenses.GetByIdAsync(message.ExpenseId);
            if (expense == null)
            {
                throw new HouseholdException($"Expense {message.ExpenseId} doesn't exists");
            }
            expense.Modify(message.ExpenseTypeId, message.Name, message.Description, message.Amount,
                message.Date, Period.Create(message.PeriodStart, message.PeriodEnd), message.Version);
            await _expenses.SaveChangesAsync(token);
        }
    }
}
