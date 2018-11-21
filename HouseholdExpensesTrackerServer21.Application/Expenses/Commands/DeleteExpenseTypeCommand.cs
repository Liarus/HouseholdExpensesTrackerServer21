using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Expenses.Commands
{
    public class DeleteExpenseTypeCommand : BaseCommand
    {
        public readonly Guid ExpenseTypeId;

        public DeleteExpenseTypeCommand(Guid expenseTypeId)
        {
            this.ExpenseTypeId = expenseTypeId;
        }
    }
}
