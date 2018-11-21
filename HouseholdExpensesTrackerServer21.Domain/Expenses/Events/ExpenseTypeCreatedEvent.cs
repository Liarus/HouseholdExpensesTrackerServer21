using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Expenses.Events
{
    public class ExpenseTypeCreatedEvent : BaseEvent
    {
        public readonly Guid UserId;

        public readonly string Name;

        public readonly string Symbol;

        public ExpenseTypeCreatedEvent(Guid expenseTypeId, Guid userId, string name, string symbol)
            :base(expenseTypeId)
        {
            this.UserId = userId;
            this.Name = name;
            this.Symbol = symbol;
        }
    }
}
