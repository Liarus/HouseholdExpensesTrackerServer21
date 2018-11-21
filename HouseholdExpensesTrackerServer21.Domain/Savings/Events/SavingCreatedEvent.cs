using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Savings.Events
{
    public class SavingCreatedEvent : BaseEvent
    {
        public readonly Guid HouseholdId;

        public readonly Guid SavingTypeId;

        public readonly string Name;

        public readonly string Description;

        public readonly decimal Amount;

        public readonly DateTime Date;

        public SavingCreatedEvent(Guid savingId, Guid householdId, Guid savingTypeId, string name, string description,
            decimal amount, DateTime date)
            :base(savingId)
        {
            this.HouseholdId = householdId;
            this.SavingTypeId = savingTypeId;
            this.Name = name;
            this.Description = description;
            this.Amount = amount;
            this.Date = date;
        }
    }
}
