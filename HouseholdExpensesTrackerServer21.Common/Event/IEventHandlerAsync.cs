using HouseholdExpensesTrackerServer21.Common.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Common.Event
{
    public interface IEventHandlerAsync<in T> : IMessageHandlerAsync<T> where T: IEvent
    {
    }
}
