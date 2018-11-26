using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Savings.Events
{
    public class SavingDeletedEvent : BaseEvent
    {
        public SavingDeletedEvent(Guid savingId) : base(savingId)
        {

        }
    }
}
