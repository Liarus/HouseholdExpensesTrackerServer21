using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Identities.Events
{
    class UserModifiedEvent : BaseEvent
    {
        public readonly string Name;

        public UserModifiedEvent(Guid userId, string name)
            : base(userId)
        {
            this.Name = name;
        }
    }
}
