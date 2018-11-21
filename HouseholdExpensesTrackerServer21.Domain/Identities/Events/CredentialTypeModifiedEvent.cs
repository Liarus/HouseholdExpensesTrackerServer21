using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Identities.Events
{
    public class CredentialTypeModifiedEvent : BaseEvent
    {
        public readonly string Code;

        public readonly string Name;

        public CredentialTypeModifiedEvent(Guid credentialTypeId, string code, string name) 
            : base(credentialTypeId)
        {
            this.Code = code;
            this.Name = name;
        }
    }
}
