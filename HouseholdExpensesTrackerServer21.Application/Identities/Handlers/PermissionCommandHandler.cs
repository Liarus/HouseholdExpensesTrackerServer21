using HouseholdExpensesTrackerServer21.Application.Identities.Commands;
using HouseholdExpensesTrackerServer21.Common.Command;
using HouseholdExpensesTrackerServer21.Common.Type;
using HouseholdExpensesTrackerServer21.Domain.Identities.Models;
using HouseholdExpensesTrackerServer21.Domain.Identities.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HouseholdExpensesTrackerServer21.Application.Identities.Handlers
{
    public class PermissionCommandHandler : ICommandHandlerAsync<CreatePermissionCommand>,
                                           ICommandHandlerAsync<ModifyPermissionCommand>,
                                           ICommandHandlerAsync<DeletePermissionCommand>
    {
        private readonly IPermissionRepository _permissions;
        public PermissionCommandHandler(IPermissionRepository permissions)
        {
            _permissions = permissions;
        }

        public async Task HandleAsync(CreatePermissionCommand message,
            CancellationToken token = default(CancellationToken))
        {
            var permission = Permission.Create(message.PermissionId, message.Name, message.Code);
            _permissions.Add(permission);
            await _permissions.SaveChangesAsync(token);
        }

        public async Task HandleAsync(ModifyPermissionCommand message,
            CancellationToken token = default(CancellationToken))
        {
            var permission = await this.GetPermissionAsync(message.PermissionId);
            permission.Modify(message.Name, message.Code, message.Version);
            await _permissions.SaveChangesAsync(token);
        }

        public async Task HandleAsync(DeletePermissionCommand message,
            CancellationToken token = default(CancellationToken))
        {
            var permission = await this.GetPermissionAsync(message.PermissionId, token);
            permission.Delete();
            _permissions.Delete(permission);
            await _permissions.SaveChangesAsync();
        }

        protected async Task<Permission> GetPermissionAsync(Guid permissionId,
            CancellationToken token = default(CancellationToken))
        {
            var permission = await _permissions.GetByIdAsync(permissionId, token);
            if (permission == null)
            {
                throw new HouseholdException($"Permission {permissionId} doesn't exists");
            }
            return permission;
        }
    }
}
