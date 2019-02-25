using HouseholdExpensesTrackerServer21.Domain.Core;
using HouseholdExpensesTrackerServer21.Domain.Identities.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Identities.Models
{
    public class Permission : AggregateRoot
    {
        public string Code { get; protected set; }

        public string Name { get; protected set; }

        public static Permission Create(Guid identity, string name, string code)
            => new Permission(identity, name, code);

        public Permission Modify(string name, string code, int version)
        {
            this.Name = name;
            this.Code = code;
            this.Version = version;
            SetSearchValue();
            this.ApplyEvent(new PermissionModifiedEvent(this.Id, code, name));
            return this;
        }

        public void Delete()
        {
            this.ApplyEvent(new PermissionDeletedEvent(this.Id));
        }

        protected override IEnumerable<object> GetSearchValues()
        {
            yield return this.Code;
            yield return this.Name;
        }

        protected Permission(Guid id, string name, string code)
        {
            this.Id = id;
            this.Name = name;
            this.Code = code;
            SetSearchValue();
            this.ApplyEvent(new PermissionCreatedEvent(id, code, name));
        }

        protected Permission()
        {

        }
    }
}
