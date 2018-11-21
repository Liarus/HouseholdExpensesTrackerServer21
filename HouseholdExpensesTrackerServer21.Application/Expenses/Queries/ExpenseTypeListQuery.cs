using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Expenses.Queries
{
    public class ExpenseTypeListQuery : BaseQuery
    {
        public readonly Guid UserId;

        public ExpenseTypeListQuery(Guid userId)
        {
            this.UserId = userId;
        }
    }
}
