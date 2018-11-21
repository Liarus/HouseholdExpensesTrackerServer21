using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.DataTransferObject.Requests
{
    public class ModifyHouseholdDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public string Description { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }

        public int Version { get; set; }
    }
}
