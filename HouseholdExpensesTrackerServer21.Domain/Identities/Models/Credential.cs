using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Identities.Models
{
    public class Credential : AuditableEntity
    {
        public Guid UserId { get; protected set; }

        public Guid CredentialTypeId { get; protected set; }

        public string Identifier { get; protected set; }

        public string Secret { get; protected set; }

        public static Credential Create(Guid id, Guid userId, Guid credentialTypeId, string identifier, string secret)
            => new Credential(id, userId, credentialTypeId, identifier, secret);

        protected override IEnumerable<object> GetSearchValues()
        {
            yield return this.Identifier;
        }

        protected Credential(Guid id,  Guid userId, Guid credentialTypeId, string identifier, string secret)
        {
            this.Id = id;
            this.UserId = userId;
            this.CredentialTypeId = credentialTypeId;
            this.Identifier = identifier;
            this.Secret = secret;
            SetSearchValue();
        }

        protected Credential()
        {

        }
    }
}
