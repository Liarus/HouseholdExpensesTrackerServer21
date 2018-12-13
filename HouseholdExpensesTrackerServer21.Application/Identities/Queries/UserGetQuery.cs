using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Identities.Queries
{
    public class UserGetQuery : BaseQuery
    {
        public readonly Guid UserId;

        public UserGetQuery(Guid userId)
        {
            this.UserId = userId;
        }
    }
}
