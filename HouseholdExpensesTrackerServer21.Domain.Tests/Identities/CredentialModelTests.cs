using HouseholdExpensesTrackerServer21.Domain.Identities.Events;
using HouseholdExpensesTrackerServer21.Domain.Identities.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HouseholdExpensesTrackerServer21.Domain.Tests.Identities
{
    [TestClass]
    public class CredentialModelTests
    {
        [TestMethod]
        public void ShouldCreateNewModel()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            Guid expectedUserId = Guid.NewGuid();
            Guid expectedCredentialTypeId = Guid.NewGuid(); 
            string expectedIdentifier = "Name";
            string expectedSecret = "Code";

            // Act
            Credential actual = Credential.Create(expectedId, expectedUserId, expectedCredentialTypeId,
                expectedIdentifier, expectedSecret);

            // Assert
            Assert.IsTrue(actual != null);
            Assert.IsTrue(actual.Id == expectedId);
            Assert.IsTrue(actual.CredentialTypeId == expectedCredentialTypeId);
            Assert.IsTrue(actual.UserId == expectedUserId);
            Assert.IsTrue(actual.Identifier == expectedIdentifier);
            Assert.IsTrue(actual.Secret == expectedSecret);
        }
    }
}
