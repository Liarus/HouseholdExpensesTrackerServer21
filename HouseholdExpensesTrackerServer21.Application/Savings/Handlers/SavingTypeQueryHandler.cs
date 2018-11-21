using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HouseholdExpensesTrackerServer21.Application.Savings.Queries;
using HouseholdExpensesTrackerServer21.Common.Query;
using HouseholdExpensesTrackerServer21.DataTransferObject.Responses;
using HouseholdExpensesTrackerServer21.Common.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace HouseholdExpensesTrackerServer21.Application.Savings.Handlers
{
    public class SavingTypeQueryHandler : IQueryHandlerAsync<SavingTypeListQuery, IEnumerable<SavingTypeDto>>
    {
        private readonly IApplicationConfiguration _configuration;

        public SavingTypeQueryHandler(IApplicationConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<SavingTypeDto>> HandleAsync(SavingTypeListQuery query,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var conn = new SqlConnection(_configuration.HouseholdConnectionString))
            {
                await conn.OpenAsync(cancellationToken);
                var types = await conn.QueryAsync<SavingTypeDto>(@"
                    SELECT Id, Name, Symbol, Version
                    FROM SavingTypes
                    WHERE UserId = @UserId
                ",
                new
                {
                    query.UserId
                });
                return types;
            }
        }
    }
}
