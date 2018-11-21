using HouseholdExpensesTrackerServer21.Domain.Core;
using HouseholdExpensesTrackerServer21.Domain.Savings.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Savings.Models
{
    public class Saving : AggregateRoot
    {
        public Guid HouseholdId { get; protected set; }

        public Guid SavingTypeId { get; protected set; }

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public decimal Amount { get; protected set; }

        public DateTime Date { get; protected set; }

        public static Saving Create(Guid id, Guid householdId, Guid savingTypeId, string name, string description,
            decimal amount, DateTime date) => new Saving(id, householdId, savingTypeId, name, description,
                amount, date);

        public Saving Modify(Guid savingTypeId, string name, string description,
            decimal amount, DateTime date, int version)
        {
            this.SavingTypeId = savingTypeId;
            this.Name = name;
            this.Description = description;
            this.Amount = amount;
            this.Date = date;
            this.Version = version;
            this.ApplyEvent(new SavingModifiedEvent(this.Id, savingTypeId, name, description,
                amount, date));
            return this;
        }

        protected Saving(Guid id, Guid householdId, Guid savingTypeId, string name, string description,
            decimal amount, DateTime date)
        {
            this.Id = id;
            this.HouseholdId = householdId;
            this.SavingTypeId = savingTypeId;
            this.Name = name;
            this.Description = description;
            this.Amount = amount;
            this.Date = date;
            this.ApplyEvent(new SavingCreatedEvent(id, householdId, savingTypeId, name, description,
                amount, date));
        }

        protected Saving()
        {

        }
    }
}
