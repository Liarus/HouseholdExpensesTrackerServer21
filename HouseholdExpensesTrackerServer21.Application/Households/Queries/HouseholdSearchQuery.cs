using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;
using static HouseholdExpensesTrackerServer21.Application.Core.Enums;

namespace HouseholdExpensesTrackerServer21.Application.Households.Queries
{
    public class HouseholdSearchQuery : BaseQuery
    {
        public readonly Guid UserId;

        public readonly int PageNumber;

        public readonly int PageSize;

        public readonly string SearchText;

        public readonly HouseholdSorting Sort;

        public HouseholdSearchQuery(
            Guid userId, int pageNumber, int pageSize, string searchText, HouseholdSorting sort)
        {
            this.UserId = userId;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.SearchText = searchText;
            this.Sort = sort;
        }
    }
}
