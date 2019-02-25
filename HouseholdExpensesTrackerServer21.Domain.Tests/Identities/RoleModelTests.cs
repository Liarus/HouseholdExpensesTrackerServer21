using HouseholdExpensesTrackerServer21.Domain.Identities.Events;
using HouseholdExpensesTrackerServer21.Domain.Identities.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace HouseholdExpensesTrackerServer21.Domain.Tests.Identities
{
    [TestClass]
    public class RoleModelTests
    {
        [TestMethod]
        public void ShouldCreateNewModel()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            string expectedName = "Name";
            string expectedCode = "Code";

            // Act
            Role actual = Role.Create(expectedId, expectedName,
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
            Role actual = Role.Create(expectedId, expectedName,
                expectedCode);

            // Assert
            Assert.IsTrue(actual != null);
            Assert.IsTrue(actual.Events != null);
            Assert.IsTrue(actual.Events.Count == 1);
            var events = actual.FlushUncommitedEvents();
            Assert.IsTrue(events != null);
            Assert.IsTrue(events.Length == 1);
            Assert.IsTrue(events[0] is RoleCreatedEvent);
            RoleCreatedEvent @event = events[0] as RoleCreatedEvent;
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

            Role actual = Role.Create(expectedId, "Name",
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

            Role actual = Role.Create(expectedId, "Name",
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
            Assert.IsTrue(events[0] is RoleModifiedEvent);
            RoleModifiedEvent @event = events[0] as RoleModifiedEvent;
            Assert.IsTrue(@event.EntityId == expectedId);
            Assert.IsTrue(@event.Name == expectedName);
            Assert.IsTrue(@event.Code == expectedCode);
        }

        [TestMethod]
        public void ShouldHaveDeletedEvent()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();

            Role actual = Role.Create(expectedId, "Name",
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
            Assert.IsTrue(events[0] is RoleDeletedEvent);
            RoleDeletedEvent @event = events[0] as RoleDeletedEvent;
            Assert.IsTrue(@event.EntityId == expectedId);
        }

        [TestMethod]
        public void ShoudAssignPermission()
        {
            // Arrange
            Guid expectedPermissionId = Guid.NewGuid();

            Role actual = Role.Create(Guid.NewGuid(), "Name",
                "Code");

            // Act
            actual.AssignPermission(expectedPermissionId);

            // Assert
            Assert.IsTrue(actual.RolePermissions != null);
            Assert.IsTrue(actual.RolePermissions.Count == 1);
            RolePermission link = actual.RolePermissions.FirstOrDefault();
            Assert.IsTrue(link != null);
            Assert.IsTrue(link.PermissionId == expectedPermissionId);
        }

        [TestMethod]
        public void ShoulHavePermissionAssignedEvent()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            Guid expectedPermissionId = Guid.NewGuid();

            Role actual = Role.Create(expectedId, "Name",
                "Code");
            actual.FlushUncommitedEvents();

            // Act
            actual.AssignPermission(expectedPermissionId);

            // Assert
            Assert.IsTrue(actual.Events != null);
            Assert.IsTrue(actual.Events.Count == 1);
            var events = actual.FlushUncommitedEvents();
            Assert.IsTrue(events != null);
            Assert.IsTrue(events.Length == 1);
            Assert.IsTrue(events[0] is PermissionAssignedEvent);
            PermissionAssignedEvent @event = events[0] as PermissionAssignedEvent;
            Assert.IsTrue(@event.EntityId == expectedPermissionId);
            Assert.IsTrue(@event.RoleId == expectedId);
        }

        [TestMethod]
        public void ShouldUnassignPermission()
        {
            // Arrange
            Guid expectedPermissionId = Guid.NewGuid();
            Role actual = Role.Create(Guid.NewGuid(), "Name",
                "Code");
            actual.AssignPermission(expectedPermissionId);

            // Act
            actual.UnassignPermission(expectedPermissionId);

            // Assert
            Assert.IsTrue(actual.RolePermissions == null || actual.RolePermissions.Count == 0);
        }

        [TestMethod]
        public void ShoulHavePermissionUnassignedEvent()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            Guid expectedPermissionId = Guid.NewGuid();
            Role actual = Role.Create(expectedId, "Name",
                "Code");
            actual.AssignPermission(expectedPermissionId);
            actual.FlushUncommitedEvents();

            // Act
            actual.UnassignPermission(expectedPermissionId);

            // Assert
            Assert.IsTrue(actual.Events != null);
            Assert.IsTrue(actual.Events.Count == 1);
            var events = actual.FlushUncommitedEvents();
            Assert.IsTrue(events != null);
            Assert.IsTrue(events.Length == 1);
            Assert.IsTrue(events[0] is PermissionUnassignedEvent);
            PermissionUnassignedEvent @event = events[0] as PermissionUnassignedEvent;
            Assert.IsTrue(@event.EntityId == expectedPermissionId);
            Assert.IsTrue(@event.RoleId == expectedId);
        }

        [TestMethod]
        public void ShouldUnassingAllPermissions()
        {
            // Arrange
            Role actual = Role.Create(Guid.NewGuid(), "Name",
                "Code");
            actual.AssignPermission(Guid.NewGuid());
            actual.AssignPermission(Guid.NewGuid());

            // Act
            actual.UnassignAllPermissions();

            // Assert
            Assert.IsTrue(actual.RolePermissions == null || actual.RolePermissions.Count == 0);
        }
    }
}
