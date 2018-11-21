using HouseholdExpensesTrackerServer21.Application.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Application.Identities.Commands
{
    public class DeleteCredentialTypeCommand : BaseCommand
    {
        public readonly Guid CredentialTypeId;

        public DeleteCredentialTypeCommand(Guid credentialTypeId)
        {
            this.CredentialTypeId = credentialTypeId;
        }
    }
}
