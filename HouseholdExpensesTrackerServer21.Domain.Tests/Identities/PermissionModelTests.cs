using HouseholdExpensesTrackerServer21.Domain.Identities.Events;
using HouseholdExpensesTrackerServer21.Domain.Identities.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HouseholdExpensesTrackerServer21.Domain.Tests.Identities
{
    [TestClass]
    public class PermissionModelTests
    {
        [TestMethod]
        public void ShouldCreateNewModel()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            string expectedName = "Name";
            string expectedCode = "Code";

            // Act
            Permission actual = Permission.Create(expectedId, expectedName,
                expectedCode);

            // Assert
            Assert.IsTrue(actual != null);
            Assert.IsTrue(actual.Id == expectedId);
            Assert.IsTrue(actual.Name == expectedName);
            Assert.IsTrue(actual.Code == expectedCode);
            Assert.IsTrue(!string.IsNullOrEmpty(actual.SearchValue));
        }

        [TestMethod]
        public void ShouldHaveCreatedEvent()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            string expectedName = "Name";
            string expectedCode = "Code";

            // Act
            Permission actual = Permission.Create(expectedId, expectedName,
                expectedCode);

            // Assert
            Assert.IsTrue(actual != null);
            Assert.IsTrue(actual.Events != null);
            Assert.IsTrue(actual.Events.Count == 1);
            var events = actual.FlushUncommitedEvents();
            Assert.IsTrue(events != null);
            Assert.IsTrue(events.Length == 1);
            Assert.IsTrue(events[0] is PermissionCreatedEvent);
            PermissionCreatedEvent @event = events[0] as PermissionCreatedEvent;
            Assert.IsTrue(@event.EntityId == expectedId);
            Assert.IsTrue(@event.Name == expectedName);
            Assert.IsTrue(@event.Code == expectedCode);
        }

        [TestMethod]
        public void ShouldModifyModel()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            string expectedName = "ModifiedName";
            string expectedCode = "ModifiedCode";

            Permission actual = Permission.Create(expectedId, "Name",
                "Code");

            // Act
            actual.Modify(expectedName, expectedCode, 1);

            // Assert
            Assert.IsTrue(actual.Id == expectedId);
            Assert.IsTrue(actual.Name == expectedName);
            Assert.IsTrue(actual.Code == expectedCode);
            Assert.IsTrue(!string.IsNullOrEmpty(actual.SearchValue));
        }

        [TestMethod]
        public void ShouldHaveModifiedEvent()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            string expectedName = "ModifiedName";
            string expectedCode = "ModifiedCode";

            Permission actual = Permission.Create(expectedId, "Name",
                "Code");
            actual.FlushUncommitedEvents();

            // Act
            actual.Modify(expectedName, expectedCode, 1);

            // Assert
            Assert.IsTrue(actual.Events != null);
            Assert.IsTrue(actual.Events.Count == 1);
            var events = actual.FlushUncommitedEvents();
            Assert.IsTrue(events != null);
            Assert.IsTrue(events.Length == 1);
            Assert.IsTrue(events[0] is PermissionModifiedEvent);
            PermissionModifiedEvent @event = events[0] as PermissionModifiedEvent;
            Assert.IsTrue(@event.EntityId == expectedId);
            Assert.IsTrue(@event.Name == expectedName);
            Assert.IsTrue(@event.Code == expectedCode);
        }

        [TestMethod]
        public void ShouldHaveDeletedEvent()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();

            Permission actual = Permission.Create(expectedId, "Name",
                "Code");
            actual.FlushUncommitedEvents();

            // Act
            actual.Delete();

            // Assert
            Assert.IsTrue(actual.Events != null);
            Assert.IsTrue(actual.Events.Count == 1);
            var events = actual.FlushUncommitedEvents();
            Assert.IsTrue(events != null);
            Assert.IsTrue(events.Length == 1);
            Assert.IsTrue(events[0] is PermissionDeletedEvent);
            PermissionDeletedEvent @event = events[0] as PermissionDeletedEvent;
            Assert.IsTrue(@event.EntityId == expectedId);
        }
    }
}
