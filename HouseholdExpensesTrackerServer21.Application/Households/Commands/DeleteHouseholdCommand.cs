using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Households.Commands
{
    public class DeleteHouseholdCommand : BaseCommand
    {
        public readonly Guid HouseholdId;

        public DeleteHouseholdCommand(Guid householdId)
        {
            this.HouseholdId = householdId;
        }
    }
}
