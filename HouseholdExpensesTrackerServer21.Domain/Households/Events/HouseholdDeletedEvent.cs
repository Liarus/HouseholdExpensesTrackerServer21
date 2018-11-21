using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Households.Events
{
    public class HouseholdDeletedEvent : BaseEvent
    {
        public HouseholdDeletedEvent(Guid householdId) : base(householdId)
        {

        }
    }
}
