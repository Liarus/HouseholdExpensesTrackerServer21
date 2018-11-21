using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Identities.Commands
{
    public class ModifyUserCommand : BaseCommand
    {
        public readonly Guid UserId;

        public readonly string Name;

        public readonly int Version;

        public ModifyUserCommand(Guid userId, string name, int version)
        {
            this.UserId = userId;
            this.Name = name;
            this.Version = version;
        }
    }
}
