using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HouseholdExpensesTrackerServer21.Application.Identities.Commands;
using HouseholdExpensesTrackerServer21.Common.Command;
using HouseholdExpensesTrackerServer21.Domain.Identities.Repositories;
using HouseholdExpensesTrackerServer21.Domain.Identities.Models;
using HouseholdExpensesTrackerServer21.Common.Type;

namespace HouseholdExpensesTrackerServer21.Application.Identities.Handlers
{
    public class RoleCommandHandler : ICommandHandlerAsync<CreateRoleCommand>,
                                      ICommandHandlerAsync<ModifyRoleCommand>,
                                      ICommandHandlerAsync<DeleteRoleCommand>,
                                      ICommandHandlerAsync<AssignPermissionCommand>,
                                      ICommandHandlerAsync<UnassignPermissionCommand>
    {
        private readonly IRoleRepository _roles;

        public RoleCommandHandler(IRoleRepository roles)
        {
            _roles = roles;
        }

        public async Task HandleAsync(CreateRoleCommand message,
            CancellationToken token = default(CancellationToken))
        {
            var role = Role.Create(message.RoleId, message.Name, message.Code);
            if (message.PermissionIds != null && message.PermissionIds.Count > 0)
            {
                foreach (var permissionId in message.PermissionIds)
                {
                    role.AssignPermission(permissionId);
                }
            }
            _roles.Add(role);
            await _roles.SaveChangesAsync(token);
        }

        public async Task HandleAsync(ModifyRoleCommand message,
            CancellationToken token = default(CancellationToken))
        {
            var role = await this.GetRoleAsync(message.RoleId);
            role.Modify(message.Name, message.Code, message.Version);
            if (message.PermissionIds == null || message.PermissionIds.Count == 0)
            {
                if (role.RolePermissions.Count != 0)
                {
                    role.UnassignAllPermissions();
                }
            }
            else
            {
                var existingPermissionIds = (from permission in role.RolePermissions
                                             select permission.PermissionId).ToList();

                foreach (var permissionId in message.PermissionIds)
                {
                    if (!role.RolePermissions.Any(e => e.PermissionId == permissionId))
                    {
                        role.AssignPermission(permissionId);
                    }
                }

                foreach (var permissionId in existingPermissionIds)
                {
                    if (!message.PermissionIds.Any(e => e == permissionId))
                    {
                        role.UnassignPermission(permissionId);
                    }
                }
            }
            await _roles.SaveChangesAsync(token);
        }

        public async Task HandleAsync(AssignPermissionCommand message,
            CancellationToken token = default(CancellationToken))
        {
            var role = await this.GetRoleAsync(message.RoleId);
            role.AssignPermission(message.PermissionId);
            await _roles.SaveChangesAsync(token);
        }

        public async Task HandleAsync(UnassignPermissionCommand message,
            CancellationToken token = default(CancellationToken))
        {
            var role = await this.GetRoleAsync(message.RoleId);
            role.UnassignPermission(message.PermissionId);
            await _roles.SaveChangesAsync(token);
        }

        public async Task HandleAsync(DeleteRoleCommand message, CancellationToken token = default(CancellationToken))
        {
            var role = await this.GetRoleAsync(message.RoleId);
            role.Delete();
            _roles.Delete(role);
            await _roles.SaveChangesAsync();
        }

        protected async Task<Role> GetRoleAsync(Guid roleId)
        {
            var role = await _roles.GetByIdAsync(roleId);
            if (role == null)
            {
                throw new HouseholdException($"Role {roleId} doesn't exists");
            }
            return role;
        }
    }
}
