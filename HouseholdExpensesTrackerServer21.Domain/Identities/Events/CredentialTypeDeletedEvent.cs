using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Identities.Events
{
    public class CredentialTypeDeletedEvent : BaseEvent
    {
        public CredentialTypeDeletedEvent(Guid credentialTypeId) : base(credentialTypeId)
        {

        }
    }
}
