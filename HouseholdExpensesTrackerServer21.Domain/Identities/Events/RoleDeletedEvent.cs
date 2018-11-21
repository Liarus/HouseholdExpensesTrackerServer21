using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Identities.Events
{
    public class RoleDeletedEvent : BaseEvent
    {
        public RoleDeletedEvent(Guid roleId) : base(roleId)
        {

        }
    }
}
