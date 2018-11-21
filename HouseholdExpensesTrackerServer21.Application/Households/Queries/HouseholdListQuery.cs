using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.DataTransferObject21.Application.Households.Queries
{
    public class HouseholdListQuery : BaseQuery
    {
        public readonly Guid UserId;

        public HouseholdListQuery(Guid userId)
        {
            this.UserId = userId;
        }
    }
}
