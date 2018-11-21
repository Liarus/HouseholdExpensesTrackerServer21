using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Identities.Commands
{
    public class DeleteRoleCommand : BaseCommand
    {
        public readonly Guid RoleId;

        public DeleteRoleCommand(Guid roleId)
        {
            this.RoleId = roleId;
        }
    }
}
