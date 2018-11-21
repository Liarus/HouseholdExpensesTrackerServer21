using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Identities.Commands
{
    public class AssignRoleCommand : BaseCommand
    {
        public readonly Guid UserId;

        public readonly Guid RoleId;

        public AssignRoleCommand(Guid userId, Guid roleId)
        {
            this.RoleId = roleId;
            this.UserId = userId;
        }
    }
}
