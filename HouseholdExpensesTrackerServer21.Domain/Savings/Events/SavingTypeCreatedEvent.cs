using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Savings.Events
{
    public class SavingTypeCreatedEvent : BaseEvent
    {
        public readonly Guid UserId;

        public readonly string Name;

        public readonly string Symbol;

        public SavingTypeCreatedEvent(Guid savingTypeId, Guid userId, string name, string symbol)
            :base(savingTypeId)
        {
            this.UserId = userId;
            this.Name = name;
            this.Symbol = symbol;
        }
    }
}
