using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HouseholdExpensesTrackerServer21.Common.Query
{
    public interface IQueryDispatcher
    {
        TResult Execute<TResult>(IQuery query);
    }
}
