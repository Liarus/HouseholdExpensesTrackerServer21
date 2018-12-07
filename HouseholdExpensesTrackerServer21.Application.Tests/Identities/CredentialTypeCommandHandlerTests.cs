using HouseholdExpensesTrackerServer21.Application.Identities.Commands;
using HouseholdExpensesTrackerServer21.Application.Identities.Handlers;
using HouseholdExpensesTrackerServer21.Domain.Identities.Models;
using HouseholdExpensesTrackerServer21.Domain.Identities.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HouseholdExpensesTrackerServer21.Application.Tests.Identities
{
    [TestClass]
    public class CredentialTypeCommandHandlerTests
    {
        [TestMethod]
        public async Task ShouldCreateNew()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            var repository = new Mock<ICredentialTypeRepository>();
            CreateCredentialTypeCommand cmd =
                new CreateCredentialTypeCommand(expectedId, "Name", "Code");

            CredentialTypeCommandHandler actual = new CredentialTypeCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            repository.Verify(e =>
                e.Add(It.Is<CredentialType>(a => a.Id == expectedId)
                ),
                Times.Once
            );
        }

        [TestMethod]
        public async Task ShouldModifyExisting()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            string expectedName = "Modified Name";
            CredentialType entity = CredentialType.Create(expectedId, "Name", "Code");
            var repository = new Mock<ICredentialTypeRepository>();
            repository.Setup(e =>
                e.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);
            ModifyCredentialTypeCommand cmd =
                new ModifyCredentialTypeCommand(expectedId, expectedName, "Code", 1);

            CredentialTypeCommandHandler actual = new CredentialTypeCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            Assert.IsTrue(entity.Id == expectedId);
            Assert.IsTrue(entity.Name == expectedName);
        }

        [TestMethod]
        public async Task ShouldDeleteExisting()
        {
            // Arrange
            CredentialType entity = CredentialType.Create(Guid.NewGuid(), "Name", "Code");
            var repository = new Mock<ICredentialTypeRepository>();
            repository.Setup(e =>
                e.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);
            DeleteCredentialTypeCommand cmd = new DeleteCredentialTypeCommand(entity.Id);

            CredentialTypeCommandHandler actual = new CredentialTypeCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            repository.Verify(e =>
                e.Delete(It.Is<CredentialType>(a => a.Equals(entity))),
                Times.Once
            );
        }
    }
}
