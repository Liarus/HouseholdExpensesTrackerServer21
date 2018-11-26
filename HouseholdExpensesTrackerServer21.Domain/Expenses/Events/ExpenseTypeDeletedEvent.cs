using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Expenses.Events
{
    public class ExpenseTypeDeletedEvent : BaseEvent
    {
        public ExpenseTypeDeletedEvent(Guid expenseTypeId) : base(expenseTypeId)
        {

        }
    }
}
