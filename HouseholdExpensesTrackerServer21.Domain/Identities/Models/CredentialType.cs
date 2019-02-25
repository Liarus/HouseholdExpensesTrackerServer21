using HouseholdExpensesTrackerServer21.Domain.Core;
using HouseholdExpensesTrackerServer21.Domain.Identities.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Identities.Models
{
    public class CredentialType : AggregateRoot
    {
        public string Code { get; protected set; }

        public string Name { get; protected set; }

        public static CredentialType Create(Guid id, string name, string code) 
            => new CredentialType(id, name, code);

        public CredentialType Modify(string name, string code, int version)
        {
            this.Name = name;
            this.Code = code;
            this.Version = version;
            SetSearchValue();
            this.ApplyEvent(new CredentialTypeModifiedEvent(this.Id, code,
                name));
            return this;
        }

        public void Delete()
        {
            this.ApplyEvent(new CredentialTypeDeletedEvent(this.Id));
        }

        protected override IEnumerable<object> GetSearchValues()
        {
            yield return this.Code;
            yield return this.Name;
        }

        protected CredentialType(Guid id, string name, string code)
        {
            this.Id = id;
            this.Name = name;
            this.Code = code;
            SetSearchValue();
            this.ApplyEvent(new CredentialTypeCreatedEvent(id, code, name));
        }

        protected CredentialType()
        {

        }
    }
}
