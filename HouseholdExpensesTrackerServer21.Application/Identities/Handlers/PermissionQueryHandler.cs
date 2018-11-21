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
    public class PermissionQueryHandler : IQueryHandlerAsync<PermissionListQuery, IEnumerable<PermissionDto>>
    {
        private readonly IApplicationConfiguration _configuration;

        public PermissionQueryHandler(IApplicationConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<PermissionDto>> HandleAsync(PermissionListQuery query,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var conn = new SqlConnection(_configuration.HouseholdConnectionString))
            {
                await conn.OpenAsync(cancellationToken);
                var permissions = await conn.QueryAsync<PermissionDto>(@"
                    SELECT Id, Code, Name, Version
                    FROM Permissions
                ");
                return permissions;
            }
        }
    }
}
