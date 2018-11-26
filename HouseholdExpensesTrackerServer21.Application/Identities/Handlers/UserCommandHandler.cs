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
    public class UserCommandHandler : ICommandHandlerAsync<CreateUserCommand>,
                                      ICommandHandlerAsync<ModifyUserCommand>,
                                      ICommandHandlerAsync<AddCredentialCommand>,
                                      ICommandHandlerAsync<AssignRoleCommand>,
                                      ICommandHandlerAsync<UnassignRoleCommand>
    {
        private readonly IUserRepository _users;

        public UserCommandHandler(IUserRepository users)
        {
            _users = users;
        }

        public async Task HandleAsync(CreateUserCommand message,
            CancellationToken token = default(CancellationToken))
        {
            var user = User.Create(message.UserId, message.Name);
            _users.Add(user);
            await _users.SaveChangesAsync(token);
        }

        public async Task HandleAsync(ModifyUserCommand message,
            CancellationToken token = default(CancellationToken))
        {
            var user = await this.GetUserAsync(message.UserId, token);
            user.Modify(message.Name, message.Version);
            await _users.SaveChangesAsync(token);
        }

        public async Task HandleAsync(AddCredentialCommand message,
            CancellationToken token = default(CancellationToken))
        {
            var user = await this.GetUserAsync(message.UserId, token);
            user.AddCredential(message.CredentialTypeId, message.Identifier, message.Secret);
            await _users.SaveChangesAsync(token);
        }

        public async Task HandleAsync(AssignRoleCommand message,
            CancellationToken token = default(CancellationToken))
        {
            var user = await this.GetUserAsync(message.UserId, token);
            user.AssignRole(message.RoleId);
            await _users.SaveChangesAsync(token);
        }

        public async Task HandleAsync(UnassignRoleCommand message,
            CancellationToken token = default(CancellationToken))
        {
            var user = await this.GetUserAsync(message.UserId, token);
            user.UnassignRole(message.RoleId);
            await _users.SaveChangesAsync(token);
        }

        protected async Task<User> GetUserAsync(Guid userId, CancellationToken token = default(CancellationToken))
        {
            var user = await _users.GetByIdAsync(userId, token);
            if (user == null)
            {
                throw new HouseholdException($"User {userId} doesn't exists");
            }
            return user;
        }
    }
}
