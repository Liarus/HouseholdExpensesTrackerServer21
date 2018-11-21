using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdExpensesTrackerServer21.Common.Type
{
    public class HouseholdException : Exception
    {
        public string Code { get; private set; }

        public HouseholdException()
        {

        }

        public HouseholdException(string message) : base(message)
        {

        }

        public HouseholdException(string code, string message) : base(message)
        {
            this.Code = code;
        }
    }
}
