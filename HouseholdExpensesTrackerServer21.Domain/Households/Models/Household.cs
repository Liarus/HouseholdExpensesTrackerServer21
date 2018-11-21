using HouseholdExpensesTrackerServer21.Domain.Core;
using HouseholdExpensesTrackerServer21.Domain.Households.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Households.Models
{
    public class Household : AggregateRoot
    {
        public Guid UserId { get; protected set; }

        public string Name { get; protected set; }

        public string Symbol { get; protected set; }

        public string Description { get; protected set; }

        public virtual Address Address { get; protected set; }

        public static Household Create(Guid id, Guid userId, string name, string symbol, string description,
            Address address) => new Household(id, userId, name, symbol, description, address);

        public Household Modify(string name, string symbol, string description, Address address, 
            int version)
        {
            this.Name = name;
            this.Symbol = symbol;
            this.Description = description;
            //this.Address = address;
            //EF core bug
            this.Address.UpdateFrom(address);
            this.Version = version;
            this.ApplyEvent(new HouseholdModifiedEvent(this.Id, name, symbol, description,  
                address.Street, address.City, address.Country, address.ZipCode));
            return this;
        }

        public void Delete()
        {
            this.ApplyEvent(new HouseholdDeletedEvent(this.Id));
        }

        protected Household(Guid id, Guid userId, string name, string symbol, string description,
            Address address)
        {
            this.Id = id;
            this.UserId = userId;
            this.Name = name;
            this.Symbol = symbol;
            this.Description = description;
            this.Address = address;
            this.ApplyEvent(new HouseholdCreatedEvent(id, userId, name, symbol, description,
                address.Street, address.City, address.Country, address.ZipCode));
        }

        protected Household()
        {

        }
    }
}
