using HouseholdExpensesTrackerServer21.Common.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Common.Command
{
    public interface ICommandHandlerAsync<in T> : IMessageHandlerAsync<T> where T : ICommand
    {
    }
}
