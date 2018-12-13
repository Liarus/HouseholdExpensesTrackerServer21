using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.DataTransferObject.Responses
{
    public class UserRoleDto
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
}
