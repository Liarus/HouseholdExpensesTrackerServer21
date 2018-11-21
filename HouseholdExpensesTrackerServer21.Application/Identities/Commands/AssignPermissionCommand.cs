using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Identities.Commands
{
    public class AssignPermissionCommand : BaseCommand
    {
        public readonly Guid PermissionId;

        public readonly Guid RoleId;

        public AssignPermissionCommand(Guid permissionId, Guid roleId)
        {
            this.RoleId = roleId;
            this.PermissionId = permissionId;
        }
    }
}
