using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Identities.Commands
{
    public class CreatePermissionCommand : BaseCommand
    {
        public readonly Guid PermissionId;

        public readonly string Name;

        public readonly string Code;

        public CreatePermissionCommand(Guid permissionId, string name, string code)
        {
            this.PermissionId = permissionId;
            this.Name = name;
            this.Code = code;
        }
    }
}
