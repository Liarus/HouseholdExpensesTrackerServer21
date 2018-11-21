using HouseholdExpensesTrackerServer21.Common.Message;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HouseholdExpensesTrackerServer21.Common.Messages
{
    public interface IMessageHandler<in T> where T : IMessage
    {
        void Handle(T message);
    }
}
