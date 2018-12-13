using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Identities.Queries
{
    public class UserGetByCredentialsQuery : BaseQuery
    {
        public readonly string Email;

        public readonly string HashedPassword;

        public UserGetByCredentialsQuery(string email, string hashedPassword)
        {
            this.Email = email;
            this.HashedPassword = hashedPassword;
        }
    }
}
