using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.DataTransferObject.Requests
{
    public class ModifyExpenseTypeDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public int Version { get; set; }
    }
}
