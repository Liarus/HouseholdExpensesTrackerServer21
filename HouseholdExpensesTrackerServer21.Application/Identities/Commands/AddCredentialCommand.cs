using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Identities.Commands
{
    public class AddCredentialCommand : BaseCommand
    {
        public readonly Guid CredentialId;

        public readonly Guid UserId;

        public readonly Guid CredentialTypeId;

        public readonly string Identifier;

        public readonly string Secret;

        public AddCredentialCommand(Guid credentialId, Guid userId, Guid credentialTypeId, string identifier, string secret)
        {
            this.UserId = userId;
            this.CredentialTypeId = credentialTypeId;
            this.Identifier = identifier;
            this.Secret = secret;
        }
    }
}
