using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Savings.Events
{
    public class SavingTypeModifiedEvent : BaseEvent
    {
        public readonly string Name;

        public readonly string Symbol;

        public SavingTypeModifiedEvent(Guid savingTypeId, string name, string symbol)
            :base(savingTypeId)
        {
            this.Name = name;
            this.Symbol = symbol;
        }
    }
}
