using HouseholdExpensesTrackerServer21.Common.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Common.Command
{
    public interface ICommandHandler<in T> : IMessageHandler<T> where T : ICommand
    {
    }
}
