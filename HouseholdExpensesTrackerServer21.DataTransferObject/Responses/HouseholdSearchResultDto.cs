using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.DataTransferObject.Responses
{
    public class HouseholdSearchResultDto
    {
        public int Count { get; set; }

        public IEnumerable<HouseholdDto> Households { get; set; }
    }
}
