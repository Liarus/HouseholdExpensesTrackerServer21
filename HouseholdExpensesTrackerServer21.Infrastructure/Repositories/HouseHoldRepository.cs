using HouseholdExpensesTrackerServer21.Domain.Households.Models;
using HouseholdExpensesTrackerServer21.Domain.Households.Repositories;
using HouseholdExpensesTrackerServer21.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Infrastructure.Repositories
{
    public class HouseholdRepository : EntityFrameworkRepository<Household>, IHouseholdRepository
    {
        public HouseholdRepository(IDbContext context) : base(context)
        {

        }
    }
}
