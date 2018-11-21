using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Identities.Commands
{
    public class ModifyRoleCommand : BaseCommand
    {
        public readonly Guid RoleId;

        public readonly string Name;

        public readonly string Code;

        public readonly ICollection<Guid> PermissionIds;

        public readonly int Version;

        public ModifyRoleCommand(Guid roleId, string name,
            string code, ICollection<Guid> permissionIds, int version)
        {
            this.RoleId = roleId;
            this.Code = code;
            this.Name = name;
            this.PermissionIds = permissionIds;
            this.Version = version;
        }
    }
}
