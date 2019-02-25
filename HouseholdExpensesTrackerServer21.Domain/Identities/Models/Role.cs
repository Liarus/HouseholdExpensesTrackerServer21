using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using HouseholdExpensesTrackerServer21.Domain.Core;
using HouseholdExpensesTrackerServer21.Domain.Identities.Events;
using HouseholdExpensesTrackerServer21.Common.Type;

namespace HouseholdExpensesTrackerServer21.Domain.Identities.Models
{
    public class Role : AggregateRoot
    {
        public string Code { get; protected set; }

        public string Name { get; protected set; }

        public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions;

        private readonly HashSet<RolePermission> _rolePermissions = new HashSet<RolePermission>();

        public static Role Create(Guid id, string name, string code)
            => new Role(id, name, code);

        public Role Modify(string name, string code, int version)
        {
            this.Name = name;
            this.Code = code;
            this.Version = version;
            SetSearchValue();
            this.ApplyEvent(new RoleModifiedEvent(this.Id, code, name));
            return this;
        }

        public void Delete()
        {
            this.ApplyEvent(new RoleDeletedEvent(this.Id));
        }

        public void AssignPermission(Guid permissionId)
        {
            var role = _rolePermissions.SingleOrDefault(e => e.PermissionId == permissionId);
            if (role != null)
            {
                throw new HouseholdException($"Permission {permissionId} is already assigned to role {this.Id}");
            }
            _rolePermissions.Add(new RolePermission { PermissionId = permissionId });
            this.ApplyEvent(new PermissionAssignedEvent(permissionId, this.Id));
        }

        public void UnassignPermission(Guid permissionId)
        {
            var permission = _rolePermissions.SingleOrDefault(e => e.PermissionId == permissionId);
            if (permission == null)
            {
                throw new HouseholdException($"Permission {permissionId} is not assigned to role {this.Id}");
            }
            _rolePermissions.Remove(permission);
            this.ApplyEvent(new PermissionUnassignedEvent(permissionId, this.Id));
        }

        public void UnassignAllPermissions()
        {
            var permissionIds = _rolePermissions.Select(e =>  e.PermissionId ).ToList();
            foreach(var permissionId in permissionIds)
            {
                this.UnassignPermission(permissionId);
            }
        }

        protected override IEnumerable<object> GetSearchValues()
        {
            yield return this.Code;
            yield return this.Name;
        }

        protected Role(Guid id, string name, string code)
        {
            this.Id = id;
            this.Name = name;
            this.Code = code;
            SetSearchValue();
            this.ApplyEvent(new RoleCreatedEvent(id, code, name));
        }

        protected Role()
        {

        }
    }
}
