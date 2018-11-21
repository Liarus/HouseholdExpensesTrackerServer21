using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HouseholdExpensesTrackerServer21.Common.Query;
using HouseholdExpensesTrackerServer21.Application.Expenses.Queries;
using HouseholdExpensesTrackerServer21.DataTransferObject.Responses;
using HouseholdExpensesTrackerServer21.Common.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace HouseholdExpensesTrackerServer21.Application.Expenses.Handlers
{
    public class ExpenseTypeQueryHandler : IQueryHandlerAsync<ExpenseTypeListQuery, IEnumerable<ExpenseTypeDto>>
    {
        private readonly IApplicationConfiguration _configuration;

        public ExpenseTypeQueryHandler(IApplicationConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<ExpenseTypeDto>> HandleAsync(ExpenseTypeListQuery query,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var conn = new SqlConnection(_configuration.HouseholdConnectionString))
            {
                await conn.OpenAsync(cancellationToken);
                var types = await conn.QueryAsync<ExpenseTypeDto>(@"
                    SELECT Id, Name, Symbol, Version
                        Address_Street AS Street, Address_City AS City, Address_Country AS Country, Address_ZipCode AS ZipCode,
                        Version
                    FROM ExpenseTypes
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
