using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Identities.Events
{
    public class RoleUnassignedEvent : BaseEvent
    {
        public readonly Guid UserId;

        public RoleUnassignedEvent(Guid roleId, Guid userId)
            : base(roleId)
        {
            this.UserId = userId;
        }
    }
}
