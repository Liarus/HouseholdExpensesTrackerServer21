using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Identities.Commands
{
    public class CreateUserCommand : BaseCommand
    {
        public readonly Guid UserId;

        public readonly string Name;

        public CreateUserCommand(Guid userId, string name)
        {
            this.UserId = userId;
            this.Name = name;
        }
    }
}
