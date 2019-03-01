using HouseholdExpensesTrackerServer21.Common.Object;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Core
{
    public abstract class Searchable : ISearchable
    {
        public string SearchValue { get; private set; }

        protected abstract IEnumerable<object> GetSearchValues();

        protected void SetSearchValue()
        {
            IEnumerable<object> searchValues = GetSearchValues();
            string searchValue = string.Empty;
            foreach (object value in searchValues)
            {
                string stringValue = String.Empty;
                if (value != null)
                {
                    if (value.GetType() == typeof(string))
                    {
                        stringValue = value as string;
                    }
                    else
                    {
                        stringValue = value.ToString();
                    }
                }

                if (!string.IsNullOrEmpty(stringValue))
                {
                    searchValue += stringValue;
                }
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                this.SearchValue = searchValue.ToLower();
            }
        }
    }
}
