using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Savings.Commands
{
    public class CreateSavingCommand : BaseCommand
    {
        public readonly Guid SavingId;

        public readonly Guid HouseholdId;

        public readonly Guid SavingTypeId;

        public readonly string Name;

        public readonly string Description;

        public readonly decimal Amount;

        public readonly DateTime Date;

        public CreateSavingCommand(Guid savingId, Guid householdId, Guid savingTypeId, string name, string description,
            decimal amount, DateTime date)
        {
            this.SavingId = savingId;
            this.HouseholdId = householdId;
            this.SavingTypeId = savingTypeId;
            this.Name = name;
            this.Description = description;
            this.Amount = amount;
            this.Date = date;
        }
    }
}
