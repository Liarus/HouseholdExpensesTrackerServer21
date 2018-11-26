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
    public class ExpenseTypeCommandHandler : ICommandHandlerAsync<CreateExpenseTypeCommand>,
                                             ICommandHandlerAsync<ModifyExpenseTypeCommand>,
                                             ICommandHandlerAsync<DeleteExpenseTypeCommand>
    {
        private readonly IExpenseTypeRepository _types;

        public ExpenseTypeCommandHandler(IExpenseTypeRepository types)
        {
            _types = types;
        }

        public async Task HandleAsync(CreateExpenseTypeCommand message, CancellationToken token = default(CancellationToken))
        {
            var type = ExpenseType.Create(message.ExpenseTypeId, message.UserId, message.Name, message.Symbol);
            _types.Add(type);
            await _types.SaveChangesAsync(token);
        }

        public async Task HandleAsync(ModifyExpenseTypeCommand message, CancellationToken token = default(CancellationToken))
        {
            var type = await this.GetSavingTypeAsync(message.ExpenseTypeId);
            type.Modify(message.Name, message.Symbol, message.Version);
            await _types.SaveChangesAsync(token);
        }

        public async Task HandleAsync(DeleteExpenseTypeCommand message, CancellationToken token = default(CancellationToken))
        {
            var type = await this.GetSavingTypeAsync(message.ExpenseTypeId);
            type.Delete();
            _types.Delete(type);
            await _types.SaveChangesAsync();
        }

        protected async Task<ExpenseType> GetSavingTypeAsync(Guid expenseTypeId, CancellationToken token = default(CancellationToken))
        {
            var type = await _types.GetByIdAsync(expenseTypeId, token);
            if (type == null)
            {
                throw new HouseholdException($"Expense Type {expenseTypeId} doesn't exists");
            }
            return type;
        }
    }
}
