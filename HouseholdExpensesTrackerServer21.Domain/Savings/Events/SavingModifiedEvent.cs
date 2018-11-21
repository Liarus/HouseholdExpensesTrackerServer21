using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Savings.Events
{
    public class SavingModifiedEvent : BaseEvent
    {
        public readonly Guid SavingTypeId;

        public readonly string Name;

        public readonly string Description;

        public readonly decimal Amount;

        public readonly DateTime Date;

        public SavingModifiedEvent(Guid savingId, Guid savingTypeId, string name, string description,
            decimal amount, DateTime date)
            :base(savingId)
        {
            this.SavingTypeId = savingTypeId;
            this.Name = name;
            this.Description = description;
            this.Amount = amount;
            this.Date = date;
        }
    }
}
