using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Savings.Commands
{
    public class CreateSavingTypeCommand : BaseCommand
    {
        public readonly Guid SavingTypeId;

        public readonly Guid UserId;

        public readonly string Name;

        public readonly string Symbol;

        public CreateSavingTypeCommand(Guid savingTypeId, Guid userId,
            string name, string symbol)
        {
            this.SavingTypeId = savingTypeId;
            this.UserId = userId;
            this.Name = name;
            this.Symbol = symbol;
        }
    }
}
