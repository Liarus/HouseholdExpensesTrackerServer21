using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Savings.Events
{
    public class SavingTypeDeletedEvent : BaseEvent
    {
        public SavingTypeDeletedEvent(Guid savingTypeId) : base(savingTypeId)
        {

        }
    }
}
