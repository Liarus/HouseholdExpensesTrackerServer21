using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Savings.Commands
{
    public class DeleteSavingTypeCommand : BaseCommand
    {
        public readonly Guid SavingTypeId;

        public DeleteSavingTypeCommand(Guid savingTypeId)
        {
            this.SavingTypeId = savingTypeId;
        }
    }
}
