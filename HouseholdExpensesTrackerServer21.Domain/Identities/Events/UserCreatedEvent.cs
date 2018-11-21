using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Identities.Events
{
    public class UserCreatedEvent : BaseEvent
    {
        public readonly string Name;

        public UserCreatedEvent(Guid userId, string name)
            : base(userId)
        {
            this.Name = name;
        }
    }
}
