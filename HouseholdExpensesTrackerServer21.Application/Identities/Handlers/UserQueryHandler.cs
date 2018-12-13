using Dapper;
using HouseholdExpensesTrackerServer21.Application.Identities.Queries;
using HouseholdExpensesTrackerServer21.Common.Configuration;
using HouseholdExpensesTrackerServer21.Common.Query;
using HouseholdExpensesTrackerServer21.DataTransferObject.Responses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace HouseholdExpensesTrackerServer21.Application.Identities.Handlers
{
    public class UserQueryHandler : IQueryHandlerAsync<UserGetQuery, UserDto>,
                                    IQueryHandlerAsync<UserGetByCredentialsQuery, UserDto>
    {
        private readonly IApplicationConfiguration _configuration;

        public UserQueryHandler(IApplicationConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<UserDto> HandleAsync(UserGetQuery query,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var conn = new SqlConnection(_configuration.HouseholdConnectionString))
            {
                await conn.OpenAsync(cancellationToken);
                var multi = await conn.QueryMultipleAsync(@"
                    SELECT Id, Name
                    FROM Users
                    WHERE UserId = @UserId;
                    SELECT r.Id, r.Code, r.Name
                    FROM UserRoles u
                        INNER JOIN Roles r ON (r.Id = u.RoleId)
                    WHERE u.UserId = @UserId;
                ",
                new
                {
                    query.UserId
                });
                UserDto user = multi.Read<UserDto>().First();
                user.Roles = multi.Read<UserRoleDto>().ToList();
                return user;
            }
        }

        public async Task<UserDto> HandleAsync(UserGetByCredentialsQuery query,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var conn = new SqlConnection(_configuration.HouseholdConnectionString))
            {
                await conn.OpenAsync(cancellationToken);
                var user = await conn.QueryFirstOrDefaultAsync<UserDto>(@"
                    SELECT u.Id, u.Name
                    FROM Users u
                        INNER JOIN Credentials c ON (c.UserId = u.Id)
                        INNER JOIN CredentialTypes t ON (t.Id = c.CredentialTypeId)
                    WHERE c.Identifier = @Email AND c.Secret = @HashedPassword
                ",
                new
                {
                    query.Email,
                    query.HashedPassword
                });
                if (user == null)
                {
                    return null;
                }

                var userRoles = await conn.QueryAsync<UserRoleDto>(@"
                    SELECT r.Id, r.Code, r.Name
                    FROM UserRoles u
                        INNER JOIN Roles r ON (r.Id = u.RoleId)
                    WHERE u.UserId = @UserId;
                ",
                new
                {
                    UserId = user.Id
                });

                user.Roles = userRoles.ToList(); ;
                return user;
            }
        }
    }
}
