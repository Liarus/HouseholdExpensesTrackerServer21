using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Identities.Events
{
    public class RoleAssignedEvent : BaseEvent
    {
        public readonly Guid UserId;

        public RoleAssignedEvent(Guid roleId, Guid userId)
            :base(roleId)
        {
            this.UserId = userId;
        }
    }
}
