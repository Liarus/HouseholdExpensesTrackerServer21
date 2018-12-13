using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.DataTransferObject.Requests
{
    public class LoginUserDto
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
