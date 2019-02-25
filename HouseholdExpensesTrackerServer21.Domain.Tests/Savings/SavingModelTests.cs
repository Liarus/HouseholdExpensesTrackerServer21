using HouseholdExpensesTrackerServer21.Domain.Savings.Events;
using HouseholdExpensesTrackerServer21.Domain.Savings.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HouseholdExpensesTrackerServer21.Domain.Tests.Savings
{
    [TestClass]
    public class SavingModelTests
    {
        [TestMethod]
        public void ShouldCreateNewModel()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            Guid expectedHouseholdId = Guid.NewGuid();
            Guid expectedSavingTypeId = Guid.NewGuid();
            string expectedName = "Name";
            string expectedDescription = "Description";
            decimal expectedAmount = 102.12m;
            DateTime expectedDate = DateTime.Now;

            // Act
            Saving actual = Saving.Create(expectedId, expectedHouseholdId, expectedSavingTypeId,
                expectedName, expectedDescription, expectedAmount, expectedDate);

            // Assert
            Assert.IsTrue(actual != null);
            Assert.IsTrue(actual.Id == expectedId);
            Assert.IsTrue(actual.HouseholdId == expectedHouseholdId);
            Assert.IsTrue(actual.SavingTypeId == expectedSavingTypeId);
            Assert.IsTrue(actual.Name == expectedName);
            Assert.IsTrue(actual.Description == expectedDescription);
            Assert.IsTrue(actual.Amount == expectedAmount);
            Assert.IsTrue(actual.Date == expectedDate);
            Assert.IsTrue(!string.IsNullOrEmpty(actual.SearchValue));
        }

        [TestMethod]
        public void ShouldHaveCreatedEvent()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            Guid expectedHouseholdId = Guid.NewGuid();
            Guid expectedSavingTypeId = Guid.NewGuid();
            string expectedName = "Name";
            string expectedDescription = "Description";
            decimal expectedAmount = 102.12m;
            DateTime expectedDate = DateTime.Now;

            // Act
            Saving actual = Saving.Create(expectedId, expectedHouseholdId, expectedSavingTypeId,
                expectedName, expectedDescription, expectedAmount, expectedDate);

            // Assert
            Assert.IsTrue(actual != null);
            Assert.IsTrue(actual.Events != null);
            Assert.IsTrue(actual.Events.Count == 1);
            var events = actual.FlushUncommitedEvents();
            Assert.IsTrue(events != null);
            Assert.IsTrue(events.Length == 1);
            Assert.IsTrue(events[0] is SavingCreatedEvent);
            SavingCreatedEvent @event = events[0] as SavingCreatedEvent;
            Assert.IsTrue(@event.EntityId == expectedId);
            Assert.IsTrue(@event.HouseholdId == expectedHouseholdId);
            Assert.IsTrue(@event.SavingTypeId == expectedSavingTypeId);
            Assert.IsTrue(@event.Description == expectedDescription);
            Assert.IsTrue(@event.Name == expectedName);
            Assert.IsTrue(@event.Date == expectedDate);
        }

        [TestMethod]
        public void ShouldModifyModel()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            Guid expectedHouseholdId = Guid.NewGuid();
            Guid expectedSavingTypeId = Guid.NewGuid();
            string expectedName = "ModifiedName";
            string expectedDescription = "ModifiedDescription";
            decimal expectedAmount = 0.12m;
            DateTime expectedDate = DateTime.Now.AddDays(1);

            // Act
            Saving actual = Saving.Create(expectedId, expectedHouseholdId, Guid.NewGuid(),
                "Name", "Description", 100.12M, DateTime.Now);
            actual.Modify(expectedSavingTypeId, expectedName, expectedDescription, expectedAmount,
                expectedDate, 1);

            // Assert
            Assert.IsTrue(actual.Id == expectedId);
            Assert.IsTrue(actual.Name == expectedName);
            Assert.IsTrue(actual.Description == expectedDescription);
            Assert.IsTrue(actual.Date == expectedDate);
            Assert.IsTrue(actual.Amount == expectedAmount);
            Assert.IsTrue(actual.SavingTypeId == expectedSavingTypeId);
            Assert.IsTrue(!string.IsNullOrEmpty(actual.SearchValue));
        }

        [TestMethod]
        public void ShouldHaveModifiedEvent()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            Guid expectedSavingTypeId = Guid.NewGuid();
            string expectedName = "ModifiedName";
            string expectedDescription = "ModifiedDescription";
            decimal expectedAmount = 0.12m;
            DateTime expectedDate = DateTime.Now.AddDays(1);

            // Act
            Saving actual = Saving.Create(expectedId, Guid.NewGuid(), Guid.NewGuid(),
                "Name", "Description", 100.12M, DateTime.Now);
            actual.FlushUncommitedEvents();
            actual.Modify(expectedSavingTypeId, expectedName, expectedDescription, expectedAmount,
                expectedDate, 1);

            // Assert
            Assert.IsTrue(actual.Events != null);
            Assert.IsTrue(actual.Events.Count == 1);
            var events = actual.FlushUncommitedEvents();
            Assert.IsTrue(events != null);
            Assert.IsTrue(events.Length == 1);
            Assert.IsTrue(events[0] is SavingModifiedEvent);
            SavingModifiedEvent @event = events[0] as SavingModifiedEvent;
            Assert.IsTrue(@event.EntityId == expectedId);
            Assert.IsTrue(@event.SavingTypeId == expectedSavingTypeId);
            Assert.IsTrue(@event.Amount == expectedAmount);
            Assert.IsTrue(@event.Description == expectedDescription);
            Assert.IsTrue(@event.Name == expectedName);
            Assert.IsTrue(@event.Date == expectedDate);
        }

        [TestMethod]
        public void ShouldHaveDeletedEvent()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();

            // Act
            Saving actual = Saving.Create(expectedId, Guid.NewGuid(), Guid.NewGuid(),
               "Name", "Description", 100.12M, DateTime.Now);
            actual.FlushUncommitedEvents();
            actual.Delete();

            // Assert
            Assert.IsTrue(actual.Events != null);
            Assert.IsTrue(actual.Events.Count == 1);
            var events = actual.FlushUncommitedEvents();
            Assert.IsTrue(events != null);
            Assert.IsTrue(events.Length == 1);
            Assert.IsTrue(events[0] is SavingDeletedEvent);
            SavingDeletedEvent @event = events[0] as SavingDeletedEvent;
            Assert.IsTrue(@event.EntityId == expectedId);
        }
    }
}
