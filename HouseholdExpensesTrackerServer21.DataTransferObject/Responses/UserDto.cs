using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.DataTransferObject.Responses
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Name { get; set;}

        public ICollection<UserRoleDto> Roles { get; set; }
    }
}
