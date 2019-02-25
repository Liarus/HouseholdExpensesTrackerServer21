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
            SetSearchValue();
            this.ApplyEvent(new HouseholdModifiedEvent(this.Id, name, symbol, description,  
                address.Street, address.City, address.Country, address.ZipCode));
            return this;
        }

        public void Delete()
        {
            this.ApplyEvent(new HouseholdDeletedEvent(this.Id));
        }

        protected override IEnumerable<object> GetSearchValues()
        {
            yield return this.Name;
            yield return this.Symbol;
            yield return this.Description;
            if (this.Address != null)
            {
                yield return this.Address.City;
                yield return this.Address.Country;
                yield return this.Address.Street;
                yield return this.Address.ZipCode;
            }
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
            SetSearchValue();
            this.ApplyEvent(new HouseholdCreatedEvent(id, userId, name, symbol, description,
                address.Street, address.City, address.Country, address.ZipCode));
        }

        protected Household()
        {

        }
    }
}
