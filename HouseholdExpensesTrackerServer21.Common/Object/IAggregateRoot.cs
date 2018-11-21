using HouseholdExpensesTrackerServer21.Common.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Common.Object
{
    public interface IAggregateRoot
    {
        IReadOnlyCollection<IEvent> Events { get; }

        void ClearEvents();

        IEvent[] FlushUncommitedEvents();
    }
}
