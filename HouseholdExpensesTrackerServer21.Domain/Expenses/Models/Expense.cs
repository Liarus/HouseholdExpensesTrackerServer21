using HouseholdExpensesTrackerServer21.Domain.Core;
using HouseholdExpensesTrackerServer21.Domain.Expenses.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Expenses.Models
{
    public class Expense : AggregateRoot
    {
        public Guid HouseholdId { get; protected set; }

        public Guid ExpenseTypeId { get; protected set; }

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public decimal Amount { get; protected set; }

        public DateTime Date { get; protected set; }

        public virtual Period Period { get; protected set; }

        public static Expense Create(Guid id, Guid householdId, Guid expenseTypeId, string name, string description,
            decimal amount, DateTime date, Period period) => new Expense(id, householdId, expenseTypeId, name, description,
                amount, date, period);

        public Expense Modify(Guid expenseTypeId, string name, string description, decimal amount,
            DateTime date, Period period, int version)
        {
            this.ExpenseTypeId = expenseTypeId;
            this.Name = name;
            this.Description = description;
            this.Amount = amount;
            this.Date = date;
            this.Period = period;
            this.Version = version;
            this.ApplyEvent(new ExpenseModifiedEvent(this.Id, expenseTypeId, name, description, amount, date,
                period.PeriodStart, period.PeriodEnd));
            return this;
        }

        protected Expense(Guid id, Guid householdId, Guid expenseTypeId, string name, string description, decimal amount,
            DateTime date, Period period)
        {
            this.Id = id;
            this.ExpenseTypeId = expenseTypeId;
            this.Name = name;
            this.Description = description;
            this.Amount = amount;
            this.Date = date;
            this.Period = period;
            this.ApplyEvent(new ExpenseCreatedEvent(id, householdId, expenseTypeId, name, description,
                amount, date, period.PeriodStart, period.PeriodEnd));
        }

        protected Expense()
        {

        }
    }
}
