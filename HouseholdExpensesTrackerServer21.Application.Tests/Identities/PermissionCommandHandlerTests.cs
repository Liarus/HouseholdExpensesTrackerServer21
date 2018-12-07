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
    public class PermissionCommandHandlerTests
    {
        [TestMethod]
        public async Task ShouldCreateNew()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            var repository = new Mock<IPermissionRepository>();
            CreatePermissionCommand cmd =
                new CreatePermissionCommand(expectedId, "Name", "Code");

            PermissionCommandHandler actual = new PermissionCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            repository.Verify(e =>
                e.Add(It.Is<Permission>(a => a.Id == expectedId)
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
            Permission entity = Permission.Create(expectedId, "Name", "Code");
            var repository = new Mock<IPermissionRepository>();
            repository.Setup(e =>
                e.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);
            ModifyPermissionCommand cmd =
                new ModifyPermissionCommand(expectedId, expectedName, "Code", 1);

            PermissionCommandHandler actual = new PermissionCommandHandler(repository.Object);

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
            Permission entity = Permission.Create(Guid.NewGuid(), "Name", "Code");
            var repository = new Mock<IPermissionRepository>();
            repository.Setup(e =>
                e.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);
            DeletePermissionCommand cmd = new DeletePermissionCommand(entity.Id);

            PermissionCommandHandler actual = new PermissionCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            repository.Verify(e =>
                e.Delete(It.Is<Permission>(a => a.Equals(entity))),
                Times.Once
            );
        }
    }
}
