using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Identities.Events
{
    public class RoleCreatedEvent : BaseEvent
    {
        public readonly string Code;

        public readonly string Name;

        public RoleCreatedEvent(Guid roleId, string code, string name) : base(roleId)
        {
            this.Code = code;
            this.Name = name;
        }
    }
}
