using HouseholdExpensesTrackerServer21.Common.Repository;
using HouseholdExpensesTrackerServer21.Domain.Expenses.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Expenses.Repositories
{
    public interface IExpenseTypeRepository : IRepository<ExpenseType>
    {
    }
}
