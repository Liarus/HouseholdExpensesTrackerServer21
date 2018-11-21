using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HouseholdExpensesTrackerServer21.Common.Query
{
    public interface IQueryDispatcherAsync
    {
        Task<TResult> ExecuteAsync<TResult>(IQuery query,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
