using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Common.Object
{
    public interface IAuditableEntity
    {
        int Version{ get; }

        void CreateAuditable(DateTime createdDate, string createdBy);

        void UpdateAuditable(DateTime updatedDate, string updatedBy);
    }
}
