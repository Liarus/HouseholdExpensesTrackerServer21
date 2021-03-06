﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using HouseholdExpensesTrackerServer21.Domain.Core;
using HouseholdExpensesTrackerServer21.Domain.Identities.Events;
using HouseholdExpensesTrackerServer21.Common.Type;

namespace HouseholdExpensesTrackerServer21.Domain.Identities.Models
{
    public class User : AggregateRoot
    {
        public string Name { get; protected set; }

        public IReadOnlyCollection<Credential> Credentials => _credentials;

        public IReadOnlyCollection<UserRole> UserRoles => _userRoles;

        private readonly List<Credential> _credentials;

        private readonly List<UserRole> _userRoles;

        public static User Create(Guid id, string name) => new User(id, name);

        public User Modify(string name, int version)
        {
            this.Name = name;
            this.Version = version;
            SetSearchValue();
            this.ApplyEvent(new UserModifiedEvent(this.Id, name));
            return this;
        }

        public void AddCredential(Guid credentialTypeId, string identifier, string secret)
        {
            Guid credentialId = Guid.NewGuid();
            _credentials.Add(Credential.Create(credentialId, this.Id, credentialTypeId, identifier, secret));
            this.ApplyEvent(new CredentialAddedEvent(credentialId,
                this.Id, credentialTypeId, identifier));
        }

        public void AssignRole(Guid roleId)
        {
            var role = _userRoles.SingleOrDefault(e => e.RoleId == roleId);
            if (role != null)
            {
                throw new HouseholdException($"Role {roleId} is already assigned to user {this.Id}");

            }
            _userRoles.Add(new UserRole { RoleId = roleId });
            this.ApplyEvent(new RoleAssignedEvent(roleId, this.Id));
        }

        public void UnassignRole(Guid roleId)
        {
            var role = _userRoles.SingleOrDefault(e => e.RoleId == roleId);
            if (role == null)
            {
                throw new HouseholdException($"Role {roleId} is not assigned to user {this.Id}");
            }
            _userRoles.Remove(role);
            this.ApplyEvent(new RoleUnassignedEvent(roleId, this.Id));
        }

        protected override IEnumerable<object> GetSearchValues()
        {
            yield return this.Name;
        }

        protected User(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
            _credentials = new List<Credential>();
            _userRoles = new List<UserRole>();
            SetSearchValue();
            this.ApplyEvent(new UserCreatedEvent(this.Id, name));
        }

        protected User()
        {
            _credentials = new List<Credential>();
            _userRoles = new List<UserRole>();
        }
    }
}
