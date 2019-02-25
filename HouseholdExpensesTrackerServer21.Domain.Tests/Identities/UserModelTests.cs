using HouseholdExpensesTrackerServer21.Domain.Identities.Events;
using HouseholdExpensesTrackerServer21.Domain.Identities.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace HouseholdExpensesTrackerServer21.Domain.Tests.Identities
{
    [TestClass]
    public class UserModelTests
    {
        [TestMethod]
        public void ShouldCreateNewModel()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            string expectedName = "Name";

            // Act
            User actual = User.Create(expectedId, expectedName);

            // Assert
            Assert.IsTrue(actual != null);
            Assert.IsTrue(actual.Id == expectedId);
            Assert.IsTrue(actual.Name == expectedName);
            Assert.IsTrue(!string.IsNullOrEmpty(actual.SearchValue));
        }

        [TestMethod]
        public void ShouldHaveCreatedEvent()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            string expectedName = "Name";

            // Act
            User actual = User.Create(expectedId, expectedName);

            // Assert
            Assert.IsTrue(actual != null);
            Assert.IsTrue(actual.Events != null);
            Assert.IsTrue(actual.Events.Count == 1);
            var events = actual.FlushUncommitedEvents();
            Assert.IsTrue(events != null);
            Assert.IsTrue(events.Length == 1);
            Assert.IsTrue(events[0] is UserCreatedEvent);
            UserCreatedEvent @event = events[0] as UserCreatedEvent;
            Assert.IsTrue(@event.EntityId == expectedId);
            Assert.IsTrue(@event.Name == expectedName);
        }

        [TestMethod]
        public void ShouldModifyModel()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            string expectedName = "ModifiedName";

            User actual = User.Create(expectedId, "Name");

            // Act
            actual.Modify(expectedName, 1);

            // Assert
            Assert.IsTrue(actual.Id == expectedId);
            Assert.IsTrue(actual.Name == expectedName);
            Assert.IsTrue(!string.IsNullOrEmpty(actual.SearchValue));
        }

        [TestMethod]
        public void ShouldHaveModifiedEvent()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            string expectedName = "ModifiedName";

            User actual = User.Create(expectedId, "Name");
            actual.FlushUncommitedEvents();

            // Act
            actual.Modify(expectedName, 1);

            // Assert
            Assert.IsTrue(actual.Events != null);
            Assert.IsTrue(actual.Events.Count == 1);
            var events = actual.FlushUncommitedEvents();
            Assert.IsTrue(events != null);
            Assert.IsTrue(events.Length == 1);
            Assert.IsTrue(events[0] is UserModifiedEvent);
            UserModifiedEvent @event = events[0] as UserModifiedEvent;
            Assert.IsTrue(@event.EntityId == expectedId);
            Assert.IsTrue(@event.Name == expectedName);
        }

        [TestMethod]
        public void ShoudAddCredential()
        {
            // Arrange
            Guid expectedPermissionId = Guid.NewGuid();
            Guid expectedCredentialType = Guid.NewGuid();
            string expectedIdentifier = "Identifier";
            string expectedSecret = "Secret";

            User actual = User.Create(Guid.NewGuid(), "Name");

            // Act
            actual.AddCredential(expectedCredentialType, expectedIdentifier,
                expectedSecret);

            // Assert
            Assert.IsTrue(actual.Credentials != null);
            Assert.IsTrue(actual.Credentials.Count == 1);
            Credential link = actual.Credentials.FirstOrDefault();
            Assert.IsTrue(link != null);
            Assert.IsTrue(link.CredentialTypeId == expectedCredentialType);
            Assert.IsTrue(link.Secret == expectedSecret);
            Assert.IsTrue(link.Identifier == expectedIdentifier);
        }

        [TestMethod]
        public void ShoudAssignRole()
        {
            // Arrange
            Guid expectedRoleId = Guid.NewGuid();

            User actual = User.Create(Guid.NewGuid(), "Name");

            // Act
            actual.AssignRole(expectedRoleId);

            // Assert
            Assert.IsTrue(actual.UserRoles != null);
            Assert.IsTrue(actual.UserRoles.Count == 1);
            UserRole link = actual.UserRoles.FirstOrDefault();
            Assert.IsTrue(link != null);
            Assert.IsTrue(link.RoleId == expectedRoleId);
        }

        [TestMethod]
        public void ShoulHaveRoleAssignedEvent()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            Guid expectedRoleId = Guid.NewGuid();

            User actual = User.Create(expectedId, "Name");
            actual.FlushUncommitedEvents();

            // Act
            actual.AssignRole(expectedRoleId);

            // Assert
            Assert.IsTrue(actual.Events != null);
            Assert.IsTrue(actual.Events.Count == 1);
            var events = actual.FlushUncommitedEvents();
            Assert.IsTrue(events != null);
            Assert.IsTrue(events.Length == 1);
            Assert.IsTrue(events[0] is RoleAssignedEvent);
            RoleAssignedEvent @event = events[0] as RoleAssignedEvent;
            Assert.IsTrue(@event.EntityId == expectedRoleId);
            Assert.IsTrue(@event.UserId == expectedId);
        }

        [TestMethod]
        public void ShouldUnassignRole()
        {
            // Arrange
            Guid expectedRoleId = Guid.NewGuid();

            User actual = User.Create(Guid.NewGuid(), "Name");
            actual.AssignRole(expectedRoleId);

            // Act
            actual.UnassignRole(expectedRoleId);

            // Assert
            Assert.IsTrue(actual.UserRoles == null || actual.UserRoles.Count == 0);
        }

        [TestMethod]
        public void ShoulHaveRoleUnassignedEvent()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            Guid expectedRoleId = Guid.NewGuid();

            User actual = User.Create(expectedId, "Name");
            actual.AssignRole(expectedRoleId);
            actual.FlushUncommitedEvents();

            // Act
            actual.UnassignRole(expectedRoleId);

            // Assert
            Assert.IsTrue(actual.Events != null);
            Assert.IsTrue(actual.Events.Count == 1);
            var events = actual.FlushUncommitedEvents();
            Assert.IsTrue(events != null);
            Assert.IsTrue(events.Length == 1);
            Assert.IsTrue(events[0] is RoleUnassignedEvent);
            RoleUnassignedEvent @event = events[0] as RoleUnassignedEvent;
            Assert.IsTrue(@event.EntityId == expectedRoleId);
            Assert.IsTrue(@event.UserId == expectedId);
        }
    }
}
