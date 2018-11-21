using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Expenses.Commands
{
    public class CreateExpenseCommand : BaseCommand
    {
        public readonly Guid ExpenseId;

        public readonly Guid HouseholdId;

        public readonly Guid ExpenseTypeId;

        public readonly string Name;

        public readonly string Description;

        public readonly decimal Amount;

        public readonly DateTime Date;

        public readonly DateTime PeriodStart;

        public readonly DateTime PeriodEnd;

        public CreateExpenseCommand(Guid expenseId, Guid householdId, Guid expenseTypeId, string name, string description,
            decimal amount, DateTime date, DateTime periodStart, DateTime periodEnd)
        {
            this.ExpenseId = expenseId;
            this.HouseholdId = householdId;
            this.ExpenseTypeId = expenseTypeId;
            this.Name = name;
            this.Description = description;
            this.Amount = amount;
            this.Date = date;
            this.PeriodStart = periodStart;
            this.PeriodEnd = periodEnd;
        }
    }
}
