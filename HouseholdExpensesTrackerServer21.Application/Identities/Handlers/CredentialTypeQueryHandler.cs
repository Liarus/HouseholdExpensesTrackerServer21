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
    public class CredentialTypeQueryHandler : IQueryHandlerAsync<CredentialTypeListQuery, IEnumerable<CredentialTypeDto>>
    {
        private readonly IApplicationConfiguration _configuration;

        public CredentialTypeQueryHandler(IApplicationConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<CredentialTypeDto>> HandleAsync(CredentialTypeListQuery query,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var conn = new SqlConnection(_configuration.HouseholdConnectionString))
            {
                await conn.OpenAsync(cancellationToken);
                var types = await conn.QueryAsync<CredentialTypeDto>(@"
                    SELECT Id, Code, Name, Description,
                        Address_Street AS Street, Address_City AS City, Address_Country AS Country, Address_ZipCode AS ZipCode,
                        Version
                    FROM Households
                    WHERE UserId = @UserId
                ");
                return types;
            }
        }
    }
}
