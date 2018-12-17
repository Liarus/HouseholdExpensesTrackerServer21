using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.DataTransferObject.Responses
{
    public class SignInDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }

        public string AccessToken { get; set; }
    }
}
