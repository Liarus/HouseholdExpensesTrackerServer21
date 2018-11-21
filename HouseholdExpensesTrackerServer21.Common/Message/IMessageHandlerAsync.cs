using HouseholdExpensesTrackerServer21.Common.Message;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HouseholdExpensesTrackerServer21.Common.Messages
{
    public interface IMessageHandlerAsync<in T> where T : IMessage
    {
        Task HandleAsync(T message, CancellationToken token = default(CancellationToken));
    }
}
