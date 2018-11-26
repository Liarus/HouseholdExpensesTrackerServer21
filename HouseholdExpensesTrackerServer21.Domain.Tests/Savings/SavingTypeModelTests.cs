using HouseholdExpensesTrackerServer21.Domain.Savings.Events;
using HouseholdExpensesTrackerServer21.Domain.Savings.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HouseholdExpensesTrackerServer21.Domain.Tests.Savings
{
    [TestClass]
    public class SavingTypeModelTests
    {
        [TestMethod]
        public void ShouldCreateNewModel()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            Guid expectedUserId = Guid.NewGuid();
            string expectedName = "Name";
            string expectedSymbol = "Symbol";

            // Act
            SavingType actual = SavingType.Create(expectedId, expectedUserId, expectedName,
                expectedSymbol);

            // Assert
            Assert.IsTrue(actual != null);
            Assert.IsTrue(actual.Id == expectedId);
            Assert.IsTrue(actual.Name == expectedName);
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

            // Act
            SavingType actual = SavingType.Create(expectedId, expectedUserId, expectedName,
                expectedSymbol);

            // Assert
            Assert.IsTrue(actual != null);
            Assert.IsTrue(actual.Events != null);
            Assert.IsTrue(actual.Events.Count == 1);
            var events = actual.FlushUncommitedEvents();
            Assert.IsTrue(events != null);
            Assert.IsTrue(events.Length == 1);
            Assert.IsTrue(events[0] is SavingTypeCreatedEvent);
            SavingTypeCreatedEvent @event = events[0] as SavingTypeCreatedEvent;
            Assert.IsTrue(@event.EntityId == expectedId);
            Assert.IsTrue(@event.UserId == expectedUserId);
            Assert.IsTrue(@event.Name == expectedName);
            Assert.IsTrue(@event.Symbol == expectedSymbol);
        }

        [TestMethod]
        public void ShouldModifyModel()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            Guid expectedUserId = Guid.NewGuid();
            string expectedName = "ModifiedName";
            string expectedSymbol = "ModifiedSymbol";

            // Act
            SavingType actual = SavingType.Create(expectedId, expectedUserId, "Name",
               "Symbol");
            actual.Modify(expectedName, expectedSymbol, 1);

            // Assert
            Assert.IsTrue(actual.Id == expectedId);
            Assert.IsTrue(actual.Name == expectedName);
            Assert.IsTrue(actual.Symbol == expectedSymbol);
        }

        [TestMethod]
        public void ShouldHaveModifiedEvent()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            string expectedName = "ModifiedName";
            string expectedSymbol = "ModifiedSymbol";

            // Act
            SavingType actual = SavingType.Create(expectedId, Guid.NewGuid(), "Name",
              "Symbol");
            actual.FlushUncommitedEvents();
            actual.Modify(expectedName, expectedSymbol, 1);

            // Assert
            Assert.IsTrue(actual.Events != null);
            Assert.IsTrue(actual.Events.Count == 1);
            var events = actual.FlushUncommitedEvents();
            Assert.IsTrue(events != null);
            Assert.IsTrue(events.Length == 1);
            Assert.IsTrue(events[0] is SavingTypeModifiedEvent);
            SavingTypeModifiedEvent @event = events[0] as SavingTypeModifiedEvent;
            Assert.IsTrue(@event.EntityId == expectedId);
            Assert.IsTrue(@event.Name == expectedName);
            Assert.IsTrue(@event.Symbol == expectedSymbol);
        }

        [TestMethod]
        public void ShouldHaveDeletedEvent()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();

            // Act
            SavingType actual = SavingType.Create(expectedId, Guid.NewGuid(), "Name",
              "Symbol");
            actual.FlushUncommitedEvents();
            actual.Delete();

            // Assert
            Assert.IsTrue(actual.Events != null);
            Assert.IsTrue(actual.Events.Count == 1);
            var events = actual.FlushUncommitedEvents();
            Assert.IsTrue(events != null);
            Assert.IsTrue(events.Length == 1);
            Assert.IsTrue(events[0] is SavingTypeDeletedEvent);
            SavingTypeDeletedEvent @event = events[0] as SavingTypeDeletedEvent;
            Assert.IsTrue(@event.EntityId == expectedId);
        }
    }
}
