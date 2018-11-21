using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Identities.Events
{
    public class PermissionUnassignedEvent : BaseEvent
    {
        public readonly Guid RoleId;

        public PermissionUnassignedEvent(Guid permissionId, Guid roleId)
            : base(permissionId)
        {
            this.RoleId = roleId;
        }
    }
}
