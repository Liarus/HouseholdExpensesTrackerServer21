using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Savings.Commands
{
    public class ModifySavingTypeCommand : BaseCommand
    {
        public readonly Guid SavingTypeId;

        public readonly string Name;

        public readonly string Symbol;

        public readonly int Version;

        public ModifySavingTypeCommand(Guid savingTypeId, string name, string symbol, int version)
        {
            this.SavingTypeId = savingTypeId;
            this.Name = name;
            this.Symbol = symbol;
            this.Version = version;
        }
    }
}
