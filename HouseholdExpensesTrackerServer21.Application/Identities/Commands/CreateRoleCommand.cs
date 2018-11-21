using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Identities.Commands
{
    public class CreateRoleCommand : BaseCommand
    {
        public readonly Guid RoleId;

        public readonly string Name;

        public readonly string Code;

        public readonly ICollection<Guid> PermissionIds;

        public CreateRoleCommand(Guid roleId, string name, string code, ICollection<Guid> permissionIds)
        {
            this.RoleId = roleId;
            this.Name = name;
            this.Code = code;
            this.PermissionIds = permissionIds;
        }
    }
}
