using HouseholdExpensesTrackerServer21.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HouseholdExpensesTrackerServer21.Domain.Households.Models
{
    public class Address : ValueObject
    {
        public string Street { get; protected set; }

        public string City { get; protected set; }

        public string Country { get; protected set; }

        public string ZipCode { get; protected set; }

        public static Address Create(string country, string zipCode, string city, string street)
            => new Address(country, zipCode, city, street);

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Street;
            yield return City;
            yield return Country;
            yield return ZipCode;
        }

        protected Address(string country, string zipCode, string city, string street)
        {
            this.Country = country;
            this.ZipCode = zipCode;
            this.City = city;
            this.Street = street;
        }
        protected Address()
        {

        }

        internal void UpdateFrom(Address other)
        {
            this.Country = other.Country;
            this.ZipCode = other.ZipCode;
            this.City = other.City;
            this.Street = other.Street;
        }
    }
}
