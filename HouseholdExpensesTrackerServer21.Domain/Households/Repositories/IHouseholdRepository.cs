using HouseholdExpensesTrackerServer21.Common.Repository;
using HouseholdExpensesTrackerServer21.Domain.Households.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Households.Repositories
{
    public interface IHouseholdRepository : IRepository<Household>
    {
    }
}
