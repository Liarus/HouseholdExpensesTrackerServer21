using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Core
{
    public static class Enums
    {
        public enum HouseholdSorting
        {
            Invalid = 0,
            NameAsc = 1,
            NameDesc = 2,
            SymbolAsc = 3,
            SymbolDesc = 4,
            DescriptionAsc = 5,
            DescriptionDesc = 6
        }
    }
}
