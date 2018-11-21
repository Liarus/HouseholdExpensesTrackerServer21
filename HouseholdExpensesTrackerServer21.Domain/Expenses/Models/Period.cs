using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Expenses.Models
{
    public class Period : ValueObject
    {
        protected const string DATETIME_STRING_FORMAT = "yyyy-MM";

        public DateTime PeriodStart { get; protected set; }

        public DateTime PeriodEnd { get; protected set; }

        public static Period Create(DateTime start, DateTime end) => new Period(start, end);


        public override string ToString()
        {
            if (this.PeriodStart.Equals(this.PeriodEnd))
            {
                return this.PeriodStart.ToString(DATETIME_STRING_FORMAT);
            }
            return $"{this.PeriodStart.ToString(DATETIME_STRING_FORMAT)} / {this.PeriodEnd.ToString(DATETIME_STRING_FORMAT)}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return PeriodStart;
            yield return PeriodEnd;
        }

        protected Period(DateTime start, DateTime end)
        {
            this.PeriodStart = start;
            this.PeriodEnd = end;
        }

        protected Period()
        {

        }
    }
}
