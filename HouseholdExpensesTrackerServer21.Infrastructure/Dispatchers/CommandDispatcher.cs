using Autofac;
using HouseholdExpensesTrackerServer21.Common.Command;
using HouseholdExpensesTrackerServer21.Common.Type;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HouseholdExpensesTrackerServer21.Infrastructure.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcherAsync
    {
        private readonly IComponentContext _componentContext;

        public CommandDispatcher(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public async Task SendAsync<TCommand>(TCommand command,
            CancellationToken cancellationToken) where TCommand : ICommand
        {
            if (_componentContext.TryResolve(out ICommandHandlerAsync<TCommand> handler))
            {
                await handler.HandleAsync(command, cancellationToken);
            }
            else
            {
                throw new HouseholdException(
                     $"Handler for command: {command.GetType().Name} in dispatcher {nameof(CommandDispatcher)} has not been found");
            }
        }
    }
}
