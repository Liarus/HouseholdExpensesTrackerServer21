using HouseholdExpensesTrackerServer21.Domain.Core;
using HouseholdExpensesTrackerServer21.Domain.Expenses.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Expenses.Models
{
    public class ExpenseType: AggregateRoot
    {
        public Guid UserId { get; protected set; }

        public string Name { get; protected set; }

        public string Symbol { get; protected set; }

        public static ExpenseType Create(Guid id, Guid userId, string name, string symbol)
            => new ExpenseType(id, userId, name, symbol);

        public ExpenseType Modify(string name, string symbol, int version)
        {
            this.Name = name;
            this.Symbol = symbol;
            this.Version = version;
            this.ApplyEvent(new ExpenseTypeModifiedEvent(this.Id, name, symbol));
            return this;
        }

        protected ExpenseType(Guid id, Guid userId, string name, string symbol)
        {
            this.Id = id;
            this.UserId = userId;
            this.Name = name;
            this.Symbol = symbol;
            this.ApplyEvent(new ExpenseTypeCreatedEvent(id, userId, name, symbol));
        }

        protected ExpenseType()
        {

        }
    }
}
