using HouseholdExpensesTrackerServer21.Domain.Households.Events;
using HouseholdExpensesTrackerServer21.Domain.Households.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HouseholdExpensesTrackerServer21.Domain.Tests.Households
{
    [TestClass]
    public class HouseholdModelTests
    {
        [TestMethod]
        public void ShouldCreateNewModel()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            Guid expectedUserId = Guid.NewGuid();
            string expectedName = "Name";
            string expectedSymbol = "Symbol";
            string expectedDescription = "Description";
            string expectedStreet = "Street";
            string expectedCity = "Katowice";
            string expectedZipCode = "43-000";
            string expectedCountry = "UK";
            Address expectedAddress = Address.Create(expectedCountry, expectedZipCode,
                expectedCity, expectedStreet);

            // Act
            Household actual = Household.Create(expectedId, expectedUserId, expectedName,
                expectedSymbol, expectedDescription, expectedAddress);

            // Assert
            Assert.IsTrue(actual != null);
            Assert.IsTrue(actual.Id == expectedId);
            Assert.IsTrue(actual.Name == expectedName);
            Assert.IsTrue(actual.Address != null);
            Assert.IsTrue(actual.Address.Equals(expectedAddress));
            Assert.IsTrue(actual.Description == expectedDescription);
            Assert.IsTrue(actual.Symbol == expectedSymbol);
        }

        [TestMethod]
        public void ShouldHaveCreatedEvent()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            Guid expectedUserId = Guid.NewGuid();
            string expectedName = "Name";
            string expectedSymbol = "Symbol";
            string expectedDescription = "Description";
            string expectedStreet = "Street";
            string expectedCity = "Katowice";
            string expectedZipCode = "43-000";
            string expectedCountry = "UK";

            // Act
            Household actual = Household.Create(expectedId, expectedUserId, expectedName,
                expectedSymbol, expectedDescription, Address.Create(expectedCountry,
                expectedZipCode, expectedCity, expectedStreet));

            // Assert
            Assert.IsTrue(actual != null);
            Assert.IsTrue(actual.Events != null);
            Assert.IsTrue(actual.Events.Count == 1);
            var events = actual.FlushUncommitedEvents();
            Assert.IsTrue(events != null);
            Assert.IsTrue(events.Length == 1);
            Assert.IsTrue(events[0] is HouseholdCreatedEvent);
            HouseholdCreatedEvent @event = events[0] as HouseholdCreatedEvent;
            Assert.IsTrue(@event.EntityId == expectedId);
            Assert.IsTrue(@event.UserId == expectedUserId);
            Assert.IsTrue(@event.City == expectedCity);
            Assert.IsTrue(@event.Country == expectedCountry);
            Assert.IsTrue(@event.Description == expectedDescription);
            Assert.IsTrue(@event.Name == expectedName);
            Assert.IsTrue(@event.Street == expectedStreet);
            Assert.IsTrue(@event.Symbol == expectedSymbol);
            Assert.IsTrue(@event.ZipCode == expectedZipCode);
        }

        [TestMethod]
        public void ShouldModifyModel()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            Guid expectedUserId = Guid.NewGuid();
            string expectedName = "ModifiedName";
            string expectedSymbol = "ModifiedSymbol";
            string expectedDescription = "ModifiedDescription";
            string expectedStreet = "Streets";
            string expectedCity = "Warsaw";
            string expectedZipCode = "43-100";
            string expectedCountry = "Poland";

            Household actual = Household.Create(expectedId, expectedUserId, "Name",
               "Symbol", "Description", Address.Create("UK",
               "43-000", "Warsaw", "Street"));

            // Act
            actual.Modify(expectedName, expectedSymbol, expectedDescription,
                Address.Create(expectedCountry, expectedZipCode, expectedCity,
                expectedStreet), 1);

            // Assert
            Assert.IsTrue(actual.Id == expectedId);
            Assert.IsTrue(actual.Name == expectedName);
            Assert.IsTrue(actual.Description == expectedDescription);
            Assert.IsTrue(actual.Symbol == expectedSymbol);
            Assert.IsTrue(actual.Address != null);
            Assert.IsTrue(actual.Address.City == expectedCity);
            Assert.IsTrue(actual.Address.Country == expectedCountry);
            Assert.IsTrue(actual.Address.Street == expectedStreet);
            Assert.IsTrue(actual.Address.ZipCode == expectedZipCode);
        }

        [TestMethod]
        public void ShouldHaveModifiedEvent()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            string expectedName = "ModifiedName";
            string expectedSymbol = "ModifiedSymbol";
            string expectedDescription = "ModifiedDescription";
            string expectedStreet = "Streets";
            string expectedCity = "Warsaw";
            string expectedZipCode = "43-100";
            string expectedCountry = "Poland";

            Household actual = Household.Create(expectedId, Guid.NewGuid(), "Name",
                "Symbol", "Description", Address.Create("UK",
                "43-000", "Warsaw", "Street"));
            actual.FlushUncommitedEvents();

            // Act
            actual.Modify(expectedName, expectedSymbol, expectedDescription,
                Address.Create(expectedCountry, expectedZipCode, expectedCity,
                expectedStreet), 1);

            // Assert
            Assert.IsTrue(actual.Events != null);
            Assert.IsTrue(actual.Events.Count == 1);
            var events = actual.FlushUncommitedEvents();
            Assert.IsTrue(events != null);
            Assert.IsTrue(events.Length == 1);
            Assert.IsTrue(events[0] is HouseholdModifiedEvent);
            HouseholdModifiedEvent @event = events[0] as HouseholdModifiedEvent;
            Assert.IsTrue(@event.EntityId == expectedId);
            Assert.IsTrue(@event.City == expectedCity);
            Assert.IsTrue(@event.Country == expectedCountry);
            Assert.IsTrue(@event.Description == expectedDescription);
            Assert.IsTrue(@event.Name == expectedName);
            Assert.IsTrue(@event.Street == expectedStreet);
            Assert.IsTrue(@event.Symbol == expectedSymbol);
            Assert.IsTrue(@event.ZipCode == expectedZipCode);
        }

        [TestMethod]
        public void ShouldHaveDeletedEvent()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();

            Household actual = Household.Create(expectedId, Guid.NewGuid(), "Name",
                "Symbol", "Description", Address.Create("UK",
                "43-000", "Warsaw", "Street"));
            actual.FlushUncommitedEvents();

            // Act
            actual.Delete();

            // Assert
            Assert.IsTrue(actual.Events != null);
            Assert.IsTrue(actual.Events.Count == 1);
            var events = actual.FlushUncommitedEvents();
            Assert.IsTrue(events != null);
            Assert.IsTrue(events.Length == 1);
            Assert.IsTrue(events[0] is HouseholdDeletedEvent);
            HouseholdDeletedEvent @event = events[0] as HouseholdDeletedEvent;
            Assert.IsTrue(@event.EntityId == expectedId);
        }
    }
}
