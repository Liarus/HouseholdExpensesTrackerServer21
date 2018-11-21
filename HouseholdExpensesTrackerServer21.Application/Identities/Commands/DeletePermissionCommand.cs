using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Identities.Commands
{
    public class DeletePermissionCommand : BaseCommand
    {
        public readonly Guid PermissionId;

        public DeletePermissionCommand(Guid permissionId)
        {
            this.PermissionId = permissionId;
        }
    }
}
