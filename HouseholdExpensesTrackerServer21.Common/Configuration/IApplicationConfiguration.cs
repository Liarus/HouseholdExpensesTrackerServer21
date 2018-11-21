using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Common.Configuration
{
    public interface IApplicationConfiguration
    {
        string HouseholdConnectionString { get; }

        int SqlCommandTimeoutSeconds { get; }
    }
}
