using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Expenses.Events
{
    public class ExpenseTypeModifiedEvent : BaseEvent
    {
        public readonly string Name;

        public readonly string Symbol;

        public ExpenseTypeModifiedEvent(Guid expenseTypeId, string name, string symbol)
            :base(expenseTypeId)
        {
            this.Name = name;
            this.Symbol = symbol;
        }
    }
}
