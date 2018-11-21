using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Identities.Events
{
    public class PermissionAssignedEvent : BaseEvent
    {
        public readonly Guid RoleId;

        public PermissionAssignedEvent(Guid permissionId, Guid roleId)
            : base(permissionId)
        {
            this.RoleId = roleId;
        }
    }
}
