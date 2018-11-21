using HouseholdExpensesTrackerServer21.Domain.Identities.Models;
using HouseholdExpensesTrackerServer21.Domain.Identities.Repositories;
using HouseholdExpensesTrackerServer21.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Infrastructure.Repositories
{
    public class PermissionRepository : EntityFrameworkRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(IDbContext context) : base(context)
        {

        }
    }
}
