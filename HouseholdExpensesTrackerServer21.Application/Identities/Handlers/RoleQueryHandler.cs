using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HouseholdExpensesTrackerServer21.Common.Query;
using HouseholdExpensesTrackerServer21.Application.Identities.Queries;
using HouseholdExpensesTrackerServer21.DataTransferObject.Responses;
using HouseholdExpensesTrackerServer21.Common.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace HouseholdExpensesTrackerServer21.Application.Identities.Handlers
{
    public class RoleQueryHandler : IQueryHandlerAsync<RoleListQuery, IEnumerable<RoleDto>>
    {
        private readonly IApplicationConfiguration _configuration;

        public RoleQueryHandler(IApplicationConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<RoleDto>> HandleAsync(RoleListQuery query,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var roleDictionary = new Dictionary<Guid, RoleDto>();

            using (var conn = new SqlConnection(_configuration.HouseholdConnectionString))
            {
                await conn.OpenAsync(cancellationToken);
                var permissions = await conn.QueryAsync<RoleDto, Guid, RoleDto>(@"
                    SELECT r.Id, r.Code, r.Name, r.Version
                    FROM Roles r
                        LEFT JOIN RolePermissions p ON (p.RoleId = r.Id)
                ",
                (role, permissionId) =>
                {
                    if (!roleDictionary.TryGetValue(role.Id, out RoleDto roleEntry))
                    {
                        roleEntry = role;
                        roleEntry.PermissionIds = new List<Guid>();
                        roleDictionary.Add(roleEntry.Id, roleEntry);
                    }
                    roleEntry.PermissionIds.Add(permissionId);
                    return roleEntry;
                },
                splitOn: "PermissionId");
                return permissions;
            }
            //var roles = await
            //    _db.Roles.Select(e => new RoleDto
            //    {
            //        Id = e.Id,
            //        Code = e.Code,
            //        Name = e.Name,
            //        PermissionIds = e.RolePermissions.Select(o => o.PermissionId).ToList(),
            //        Version = e.Version
            //    }).AsNoTracking().ToListAsync();
            //return roles;
        }
    }
}
