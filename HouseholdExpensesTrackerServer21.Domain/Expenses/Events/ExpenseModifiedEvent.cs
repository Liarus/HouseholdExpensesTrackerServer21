using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Expenses.Events
{
    public class ExpenseModifiedEvent : BaseEvent
    {
        public readonly Guid ExpenseTypeId;

        public readonly string Name;

        public readonly string Description;

        public readonly decimal Amount;

        public readonly DateTime Date;

        public readonly DateTime PeriodStart;

        public readonly DateTime PeriodEnd;

        public ExpenseModifiedEvent(Guid expenseId, Guid expenseTypeId, string name, string description,
            decimal amount, DateTime date, DateTime periodStart, DateTime periodEnd)
            :base(expenseId)
        {
            this.ExpenseTypeId = expenseTypeId;
            this.Name = name;
            this.Description = description;
            this.Amount = amount;
            this.Date = date;
            this.PeriodStart = periodStart;
            this.PeriodEnd = periodEnd;
        }
    }
}
