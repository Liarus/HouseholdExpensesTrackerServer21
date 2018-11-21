using HouseholdExpensesTrackerServer21.Common.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Core
{
    public abstract class BaseEvent : IEvent
    {
        public readonly Guid EntityId;

        public readonly DateTimeOffset TimeStamp;

        protected BaseEvent(Guid entityId)
        {
            this.EntityId = entityId;
            this.TimeStamp = DateTime.Now;
        }
    }
}
