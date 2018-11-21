using HouseholdExpensesTrackerServer21.Common.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Common.Event
{
    public interface IEventHandler<in T> : IMessageHandler<T> where T: IEvent
    {
    }
}
