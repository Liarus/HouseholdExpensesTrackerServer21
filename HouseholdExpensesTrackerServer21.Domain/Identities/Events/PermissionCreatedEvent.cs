using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Identities.Events
{
    public class PermissionCreatedEvent : BaseEvent
    {
        public readonly string Code;

        public readonly string Name;

        public PermissionCreatedEvent(Guid permissionId, string code, string name) : base(permissionId)
        {
            this.Code = code;
            this.Name = name;
        }
    }
}
