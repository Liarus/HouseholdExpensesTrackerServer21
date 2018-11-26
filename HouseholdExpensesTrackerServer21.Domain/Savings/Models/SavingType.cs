using HouseholdExpensesTrackerServer21.Domain.Core;
using HouseholdExpensesTrackerServer21.Domain.Savings.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Savings.Models
{
    public class SavingType : AggregateRoot
    {
        public Guid UserId { get; protected set; }

        public string Name { get; protected set; }

        public string Symbol { get; protected set; }

        public static SavingType Create(Guid id, Guid userId, string name, string symbol)
            => new SavingType(id, userId, name, symbol);

        public SavingType Modify(string name, string symbol, int version)
        {
            this.Name = name;
            this.Symbol = symbol;
            this.Version = version;
            this.ApplyEvent(new SavingTypeModifiedEvent(this.Id, name, symbol));
            return this;
        }

        public void Delete()
        {
            this.ApplyEvent(new SavingTypeDeletedEvent(this.Id));
        }

        protected SavingType(Guid id, Guid userId, string name, string symbol)
        {
            this.Id = id;
            this.UserId = userId;
            this.Name = name;
            this.Symbol = symbol;
            this.ApplyEvent(new SavingTypeCreatedEvent(id, userId, name, symbol));
        }

        protected SavingType()
        {

        }
    }
}
