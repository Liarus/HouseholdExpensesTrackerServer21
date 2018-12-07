using HouseholdExpensesTrackerServer21.Application.Identities.Commands;
using HouseholdExpensesTrackerServer21.Application.Identities.Handlers;
using HouseholdExpensesTrackerServer21.Domain.Identities.Models;
using HouseholdExpensesTrackerServer21.Domain.Identities.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HouseholdExpensesTrackerServer21.Application.Tests.Identities
{
    [TestClass]
    public class UserCommandHandlerTests
    {
        [TestMethod]
        public async Task ShouldCreateNew()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            var repository = new Mock<IUserRepository>();
            CreateUserCommand cmd =
                new CreateUserCommand(expectedId, "Name");

            UserCommandHandler actual = new UserCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            repository.Verify(e =>
                e.Add(It.Is<User>(a => a.Id == expectedId)
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
            User entity = User.Create(expectedId, "Name");
            var repository = new Mock<IUserRepository>();
            repository.Setup(e =>
                e.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);
            ModifyUserCommand cmd =
                new ModifyUserCommand(expectedId, expectedName, 1);

            UserCommandHandler actual = new UserCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            Assert.IsTrue(entity.Id == expectedId);
            Assert.IsTrue(entity.Name == expectedName);
        }

        [TestMethod]
        public async Task ShouldAssingRole()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            Guid expectedRoleId = Guid.NewGuid();
            User entity = User.Create(expectedId, "Name");
            var repository = new Mock<IUserRepository>();
            repository.Setup(e =>
                e.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);
            AssignRoleCommand cmd = new AssignRoleCommand(expectedId, expectedRoleId);

            UserCommandHandler actual = new UserCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            Assert.IsTrue(entity.UserRoles != null);
            Assert.IsTrue(entity.UserRoles.Any(e => e.RoleId == expectedRoleId));
        }

        [TestMethod]
        public async Task ShouldUnassingRole()
        {
            // Arrange
            Guid roleId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            User entity = User.Create(userId, "Name");
            entity.AssignRole(roleId);
            var repository = new Mock<IUserRepository>();
            repository.Setup(e =>
                e.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);
            UnassignRoleCommand cmd = new UnassignRoleCommand(userId, roleId);

            UserCommandHandler actual = new UserCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            Assert.IsTrue(entity.UserRoles == null || entity.UserRoles.Count == 0);
        }
    }
}
