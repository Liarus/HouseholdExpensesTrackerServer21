using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Identities.Commands
{
    public class ModifyPermissionCommand : BaseCommand
    {
        public readonly Guid PermissionId;

        public readonly string Name;

        public readonly string Code;

        public readonly int Version;

        public ModifyPermissionCommand(Guid permissionId, string name,
            string code, int version)
        {
            this.PermissionId = permissionId;
            this.Code = code;
            this.Name = name;
            this.Version = version;
        }
    }
}
