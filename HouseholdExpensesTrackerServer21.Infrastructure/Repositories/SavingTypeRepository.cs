using HouseholdExpensesTrackerServer21.Domain.Savings.Models;
using HouseholdExpensesTrackerServer21.Domain.Savings.Repositories;
using HouseholdExpensesTrackerServer21.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Infrastructure.Repositories
{
    public class SavingTypeRepository : EntityFrameworkRepository<SavingType>, ISavingTypeRepository
    {
        public SavingTypeRepository(IDbContext context) : base(context)
        {

        }
    }
}
