using Autofac;
using HouseholdExpensesTrackerServer21.Common.Query;
using HouseholdExpensesTrackerServer21.Common.Type;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HouseholdExpensesTrackerServer21.Infrastructure.Dispatchers
{
    public class QueryDispatcher : IQueryDispatcherAsync
    {
        private readonly IComponentContext _componentContext;

        public QueryDispatcher(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public async Task<TResult> ExecuteAsync<TResult>(IQuery query,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var handlerType =
                typeof(IQueryHandlerAsync<,>).MakeGenericType(query.GetType(), typeof(TResult));

            if (_componentContext.TryResolve(handlerType, out dynamic handler))
            {
                return await handler.HandleAsync((dynamic)query, cancellationToken);
            }

            throw new HouseholdException(
                $"Handler for query: {query.GetType().Name} in dispatcher {nameof(QueryDispatcher)} has not been found");
        }

    }
}
