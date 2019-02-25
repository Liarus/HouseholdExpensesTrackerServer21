using HouseholdExpensesTrackerServer21.Domain.Expenses.Events;
using HouseholdExpensesTrackerServer21.Domain.Expenses.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HouseholdExpensesTrackerServer21.Domain.Tests.Expenses
{
    [TestClass]
    public class ExpenseTypeModelTests
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
            ExpenseType actual = ExpenseType.Create(expectedId, expectedUserId, expectedName,
                expectedSymbol);

            // Assert
            Assert.IsTrue(actual != null);
            Assert.IsTrue(actual.Id == expectedId);
            Assert.IsTrue(actual.Name == expectedName);
            Assert.IsTrue(actual.Symbol == expectedSymbol);
            Assert.IsTrue(!string.IsNullOrEmpty(actual.SearchValue));
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
            ExpenseType actual = ExpenseType.Create(expectedId, expectedUserId, expectedName,
                expectedSymbol);

            // Assert
            Assert.IsTrue(actual != null);
            Assert.IsTrue(actual.Events != null);
            Assert.IsTrue(actual.Events.Count == 1);
            var events = actual.FlushUncommitedEvents();
            Assert.IsTrue(events != null);
            Assert.IsTrue(events.Length == 1);
            Assert.IsTrue(events[0] is ExpenseTypeCreatedEvent);
            ExpenseTypeCreatedEvent @event = events[0] as ExpenseTypeCreatedEvent;
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

            ExpenseType actual = ExpenseType.Create(expectedId, expectedUserId, "Name",
                "Symbol");

            // Act
            actual.Modify(expectedName, expectedSymbol, 1);

            // Assert
            Assert.IsTrue(actual.Id == expectedId);
            Assert.IsTrue(actual.Name == expectedName);
            Assert.IsTrue(actual.Symbol == expectedSymbol);
            Assert.IsTrue(!string.IsNullOrEmpty(actual.SearchValue));
        }

        [TestMethod]
        public void ShouldHaveModifiedEvent()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            string expectedName = "ModifiedName";
            string expectedSymbol = "ModifiedSymbol";

            ExpenseType actual = ExpenseType.Create(expectedId, Guid.NewGuid(), "Name",
                "Symbol");
            actual.FlushUncommitedEvents();

            // Act
            actual.Modify(expectedName, expectedSymbol, 1);

            // Assert
            Assert.IsTrue(actual.Events != null);
            Assert.IsTrue(actual.Events.Count == 1);
            var events = actual.FlushUncommitedEvents();
            Assert.IsTrue(events != null);
            Assert.IsTrue(events.Length == 1);
            Assert.IsTrue(events[0] is ExpenseTypeModifiedEvent);
            ExpenseTypeModifiedEvent @event = events[0] as ExpenseTypeModifiedEvent;
            Assert.IsTrue(@event.EntityId == expectedId);
            Assert.IsTrue(@event.Name == expectedName);
            Assert.IsTrue(@event.Symbol == expectedSymbol);
        }

        [TestMethod]
        public void ShouldHaveDeletedEvent()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();

            ExpenseType actual = ExpenseType.Create(expectedId, Guid.NewGuid(), "Name",
                "Symbol");
            actual.FlushUncommitedEvents();

            // Act
            actual.Delete();

            // Assert
            Assert.IsTrue(actual.Events != null);
            Assert.IsTrue(actual.Events.Count == 1);
            var events = actual.FlushUncommitedEvents();
            Assert.IsTrue(events != null);
            Assert.IsTrue(events.Length == 1);
            Assert.IsTrue(events[0] is ExpenseTypeDeletedEvent);
            ExpenseTypeDeletedEvent @event = events[0] as ExpenseTypeDeletedEvent;
            Assert.IsTrue(@event.EntityId == expectedId);
        }
    }
}
