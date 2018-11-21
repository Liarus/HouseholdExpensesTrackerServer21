using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HouseholdExpensesTrackerServer21.Common.Query;
using HouseholdExpensesTrackerServer21.DataTransferObject21.Application.Households.Queries;
using HouseholdExpensesTrackerServer21.DataTransferObject.Responses;
using HouseholdExpensesTrackerServer21.Common.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace HouseholdExpensesTrackerServer21.Application.Households.Handlers
{
    public class HouseholdQueryHandler : IQueryHandlerAsync<HouseholdListQuery, IEnumerable<HouseholdDto>>
    {
        private readonly IApplicationConfiguration _configuration;

        public HouseholdQueryHandler(IApplicationConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<HouseholdDto>> HandleAsync(HouseholdListQuery query,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var conn = new SqlConnection(_configuration.HouseholdConnectionString))
            {
                await conn.OpenAsync(cancellationToken);
                var households = await conn.QueryAsync<HouseholdDto>(@"
                    SELECT Id, Name, Symbol, Description,
                        Address_Street AS Street, Address_City AS City, Address_Country AS Country, Address_ZipCode AS ZipCode,
                        Version
                    FROM Households
                    WHERE UserId = @UserId
                ",
                new
                {
                    query.UserId
                });
                return households;
            }
        }
    }
}
