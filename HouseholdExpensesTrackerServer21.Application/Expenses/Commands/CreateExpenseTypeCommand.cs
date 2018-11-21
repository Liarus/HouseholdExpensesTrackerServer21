using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Expenses.Commands
{
    public class CreateExpenseTypeCommand : BaseCommand
    {
        public readonly Guid ExpenseTypeId;

        public readonly Guid UserId;

        public readonly string Name;

        public readonly string Symbol;

        public CreateExpenseTypeCommand(Guid expenseTypeId, Guid userId, string name, string symbol)
        {
            this.ExpenseTypeId = expenseTypeId;
            this.UserId = userId;
            this.Name = name;
            this.Symbol = symbol;
        }
    }
}
