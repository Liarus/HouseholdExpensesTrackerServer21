using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Expenses.Commands
{
    public class ModifyExpenseTypeCommand : BaseCommand
    {
        public readonly Guid ExpenseTypeId;

        public readonly string Name;

        public readonly string Symbol;

        public readonly int Version;

        public ModifyExpenseTypeCommand(Guid expenseTypeId, string name, string symbol, int version)
        {
            this.ExpenseTypeId = expenseTypeId;
            this.Name = name;
            this.Symbol = symbol;
            this.Version = version;
        }
    }
}
