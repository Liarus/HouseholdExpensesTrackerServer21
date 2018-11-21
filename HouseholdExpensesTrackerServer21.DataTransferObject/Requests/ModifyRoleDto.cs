using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.DataTransferObject.Requests
{
    public class ModifyRoleDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public ICollection<Guid> PermissionIds { get; set; }

        public int Version { get; set; }
    }
}
