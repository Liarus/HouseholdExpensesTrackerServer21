using HouseholdExpensesTrackerServer21.Common.Event;
using HouseholdExpensesTrackerServer21.Common.Object;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Core
{
    public abstract class AggregateRoot : AuditableEntity, IAggregateRoot
    {

        public IReadOnlyCollection<IEvent> Events => _events;

        protected readonly List<IEvent> _events = new List<IEvent>();

        public void ClearEvents() => _events.Clear();

        public IEvent[] FlushUncommitedEvents()
        {
            var events = _events.ToArray();
            _events.Clear();
            return events;
        }

        protected void ApplyEvent(IEvent @event) => _events.Add(@event);
    }
}
