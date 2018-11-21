using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HouseholdExpensesTrackerServer21.Common.Command;
using HouseholdExpensesTrackerServer21.Common.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseholdExpensesTrackerServer21.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly ICommandDispatcherAsync _commandDispatcher;

        private readonly IQueryDispatcherAsync _queryDispatcher;

        public BaseController(ICommandDispatcherAsync commandDispatcher,
            IQueryDispatcherAsync queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        protected async Task SendCommandAsync<TCommand>(TCommand command) where TCommand : ICommand
            => await _commandDispatcher.SendAsync<TCommand>(command, default(CancellationToken));

        protected async Task<TResult> GetQueryAsync<TResult>(IQuery query)
            => await _queryDispatcher.ExecuteAsync<TResult>(query, default(CancellationToken));
    }
}