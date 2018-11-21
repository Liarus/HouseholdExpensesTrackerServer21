using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Savings.Commands
{
    public class ModifySavingCommand : BaseCommand
    {
        public readonly Guid SavingId;

        public readonly Guid SavingTypeId;

        public readonly string Name;

        public readonly string Description;

        public readonly decimal Amount;

        public readonly DateTime Date;

        public readonly int Version;

        public ModifySavingCommand(Guid savingId, Guid savingTypeId, string name, string description,
            decimal amount, DateTime date, int version)
        {
            this.SavingId = savingId;
            this.SavingTypeId = savingTypeId;
            this.Name = name;
            this.Description = description;
            this.Amount = amount;
            this.Date = date;
            this.Version = version;
        }
    }
}
