using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.DataTransferObject.Responses
{
    public class ExpenseTypeDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public int Version { get; set; }
    }
}
