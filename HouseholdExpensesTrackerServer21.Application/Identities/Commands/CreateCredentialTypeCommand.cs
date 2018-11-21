using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Identities.Commands
{
    public class CreateCredentialTypeCommand : BaseCommand
    {
        public readonly Guid CredentialTypeId;

        public readonly string Name;

        public readonly string Code;

        public CreateCredentialTypeCommand(Guid credentialTypeId, string name, string code)
        {
            this.CredentialTypeId = credentialTypeId;
            this.Name = name;
            this.Code = code;
        }
    }
}
