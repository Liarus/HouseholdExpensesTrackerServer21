using HouseholdExpensesTrackerServer21.Domain.Expenses.Models;
using HouseholdExpensesTrackerServer21.Domain.Expenses.Repositories;
using HouseholdExpensesTrackerServer21.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Infrastructure.Repositories
{
    public class ExpenseTypeRepository : EntityFrameworkRepository<ExpenseType>, IExpenseTypeRepository
    {
        public ExpenseTypeRepository(IDbContext context) : base(context)
        {

        }
    }
}
