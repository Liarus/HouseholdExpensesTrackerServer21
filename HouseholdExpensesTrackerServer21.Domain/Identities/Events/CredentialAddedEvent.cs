using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Identities.Events
{
    public class CredentialAddedEvent : BaseEvent
    {
        public readonly Guid UserId;

        public readonly Guid CredentialTypeId;

        public readonly string Identifier;

        public CredentialAddedEvent(Guid credentialId, Guid userId, Guid credentialTypeId, string identifier)
            : base(credentialId)
        {
            this.UserId = userId;
            this.CredentialTypeId = credentialTypeId;
            this.Identifier = identifier;
        }
    }
}
