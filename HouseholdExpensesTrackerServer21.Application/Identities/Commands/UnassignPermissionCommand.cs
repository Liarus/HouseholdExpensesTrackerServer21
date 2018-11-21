using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Identities.Commands
{
    public class UnassignPermissionCommand : BaseCommand
    {
        public readonly Guid PermissionId;

        public readonly Guid RoleId;

        public UnassignPermissionCommand(Guid permissionId, Guid roleId)
        {
            this.RoleId = roleId;
            this.PermissionId = permissionId;
        }
    }
}
