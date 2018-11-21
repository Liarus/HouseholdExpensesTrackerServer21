using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Savings.Queries
{
    public class SavingTypeListQuery : BaseQuery
    {
        public readonly Guid UserId;

        public SavingTypeListQuery(Guid userId)
        {
            this.UserId = userId;
        }
    }
}
