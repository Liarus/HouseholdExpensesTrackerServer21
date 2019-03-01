using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HouseholdExpensesTrackerServer21.Common.Query;
using HouseholdExpensesTrackerServer21.DataTransferObject.Responses;
using HouseholdExpensesTrackerServer21.Common.Configuration;
using System.Data.SqlClient;
using Dapper;
using HouseholdExpensesTrackerServer21.Application.Households.Queries;

namespace HouseholdExpensesTrackerServer21.Application.Households.Handlers
{
    public class HouseholdQueryHandler : IQueryHandlerAsync<HouseholdListQuery, IEnumerable<HouseholdDto>>,
                                         IQueryHandlerAsync<HouseholdSearchQuery, HouseholdSearchResultDto>
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

        public async Task<HouseholdSearchResultDto> HandleAsync(HouseholdSearchQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var conn = new SqlConnection(_configuration.HouseholdConnectionString))
            {
                await conn.OpenAsync(cancellationToken);
                var multi = await conn.QueryMultipleAsync(@"
                    SELECT COUNT(*) 
                    FROM Households
                    WHERE UserId = @UserId AND @SearchText IS NULL OR @SearchText = '' OR SearchValue LIKE '%'+@SearchText+'%'
                    SELECT Id, Name, Symbol, Description,
                        Address_Street AS Street, Address_City AS City, Address_Country AS Country, Address_ZipCode AS ZipCode,
                        Version
                    FROM Households
                    WHERE UserId = @UserId AND @SearchText IS NULL OR @SearchText = '' OR SearchValue LIKE '%'+@SearchText+'%'
                    ORDER BY 
                        CASE WHEN @Sort = 1 THEN Name END ASC,
                        CASE WHEN @Sort = 2 THEN Name END DESC,
                        CASE WHEN @Sort = 3 THEN Symbol END ASC,
                        CASE WHEN @Sort = 4 THEN Symbol END DESC,
                        CASE WHEN @Sort = 5 THEN Description END ASC,
                        CASE WHEN @Sort = 6 THEN Description END DESC
                    OFFSET @PageSize * (@PageNumber - 1) ROWS
                    FETCH NEXT @PageSize ROWS ONLY
                ",
                new
                {
                    query.UserId,
                    query.PageNumber,
                    query.PageSize,
                    query.Sort,
                    query.SearchText
                });
                HouseholdSearchResultDto result = new HouseholdSearchResultDto
                {
                    Count = multi.Read<int>().First(),
                    Households = multi.Read<HouseholdDto>().ToList()
                };
                return result;
            }
        }
    }
}
