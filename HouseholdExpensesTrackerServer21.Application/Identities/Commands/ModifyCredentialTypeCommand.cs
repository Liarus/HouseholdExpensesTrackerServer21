using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Identities.Commands
{
    public class ModifyCredentialTypeCommand : BaseCommand
    {
        public readonly Guid CredentialTypeId;

        public readonly string Name;

        public readonly string Code;

        public readonly int Version;

        public ModifyCredentialTypeCommand(Guid credentialTypeId, string name,
            string code, int version)
        {
            this.CredentialTypeId = credentialTypeId;
            this.Code = code;
            this.Name = name;
            this.Version = version;
        }
    }
}
