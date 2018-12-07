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
    public class RoleCommandHandlerTests
    {
        [TestMethod]
        public async Task ShouldCreateNew()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            var repository = new Mock<IRoleRepository>();
            CreateRoleCommand cmd =
                new CreateRoleCommand(expectedId, "Name", "Code", null);

            RoleCommandHandler actual = new RoleCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            repository.Verify(e =>
                e.Add(It.Is<Role>(a => a.Id == expectedId)
                ),
                Times.Once
            );
        }

        [TestMethod]
        public async Task ShouldCreateNewWithPermissions()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            ICollection<Guid> expectedPermissionIds = new List<Guid>
            {
                Guid.NewGuid(),
                Guid.NewGuid()
            };
            var repository = new Mock<IRoleRepository>();
            CreateRoleCommand cmd =
                new CreateRoleCommand(expectedId, "Name", "Code", expectedPermissionIds);

            RoleCommandHandler actual = new RoleCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            repository.Verify(e =>
                e.Add(It.Is<Role>(a => a.Id == expectedId && a.RolePermissions != null &&
                    a.RolePermissions.Count == expectedPermissionIds.Count)
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
            Role entity = Role.Create(expectedId, "Name", "Code");
            var repository = new Mock<IRoleRepository>();
            repository.Setup(e =>
                e.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);
            ModifyRoleCommand cmd =
                new ModifyRoleCommand(expectedId, expectedName, "Code", null, 1);

            RoleCommandHandler actual = new RoleCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            Assert.IsTrue(entity.Id == expectedId);
            Assert.IsTrue(entity.Name == expectedName);
        }

        [TestMethod]
        public async Task ShouldModifyExistingAndAddPermissions()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            string expectedName = "Modified Name";
            ICollection<Guid> expectedPermissionIds = new List<Guid>
            {
                Guid.NewGuid(),
                Guid.NewGuid()
            };
            Role entity = Role.Create(expectedId, "Name", "Code");
            var repository = new Mock<IRoleRepository>();
            repository.Setup(e =>
                e.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);
            ModifyRoleCommand cmd =
                new ModifyRoleCommand(expectedId, expectedName, "Code", expectedPermissionIds, 1);

            RoleCommandHandler actual = new RoleCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            Assert.IsTrue(entity.Id == expectedId);
            Assert.IsTrue(entity.Name == expectedName);
            Assert.IsTrue(entity.RolePermissions != null);
            Assert.IsTrue(entity.RolePermissions.Count == expectedPermissionIds.Count);
        }

        [TestMethod]
        public async Task ShouldModifyExistingAndDeleteExistsingPermissions()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            string expectedName = "Modified Name";
            Role entity = Role.Create(expectedId, "Name", "Code");
            entity.AssignPermission(Guid.NewGuid());
            entity.AssignPermission(Guid.NewGuid());
            var repository = new Mock<IRoleRepository>();
            repository.Setup(e =>
                e.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);
            ModifyRoleCommand cmd =
                new ModifyRoleCommand(expectedId, expectedName, "Code", null, 1);

            RoleCommandHandler actual = new RoleCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            Assert.IsTrue(entity.Id == expectedId);
            Assert.IsTrue(entity.Name == expectedName);
            Assert.IsTrue(entity.RolePermissions == null || entity.RolePermissions.Count == 0);
        }

        [TestMethod]
        public async Task ShouldDeleteExisting()
        {
            // Arrange
            Role entity = Role.Create(Guid.NewGuid(), "Name", "Code");
            var repository = new Mock<IRoleRepository>();
            repository.Setup(e =>
                e.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);
            DeleteRoleCommand cmd = new DeleteRoleCommand(entity.Id);

            RoleCommandHandler actual = new RoleCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            repository.Verify(e =>
                e.Delete(It.Is<Role>(a => a.Equals(entity))),
                Times.Once
            );
        }

        [TestMethod]
        public async Task ShouldAssingPermission()
        {
            // Arrange
            Guid expectedPermissionId = Guid.NewGuid();
            Guid roleId = Guid.NewGuid();
            Role entity = Role.Create(roleId, "Name", "Code");
            var repository = new Mock<IRoleRepository>();
            repository.Setup(e =>
                e.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);
            AssignPermissionCommand cmd = new AssignPermissionCommand(expectedPermissionId, roleId);

            RoleCommandHandler actual = new RoleCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            Assert.IsTrue(entity.RolePermissions != null);
            Assert.IsTrue(entity.RolePermissions.Any(e => e.PermissionId == expectedPermissionId));
        }

        [TestMethod]
        public async Task ShouldUnassingPermission()
        {
            // Arrange
            Guid permissionId = Guid.NewGuid();
            Guid roleId = Guid.NewGuid();
            Role entity = Role.Create(roleId, "Name", "Code");
            entity.AssignPermission(permissionId);
            var repository = new Mock<IRoleRepository>();
            repository.Setup(e =>
                e.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);
            UnassignPermissionCommand cmd = new UnassignPermissionCommand(permissionId, roleId);

            RoleCommandHandler actual = new RoleCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            Assert.IsTrue(entity.RolePermissions == null || entity.RolePermissions.Count == 0);
        }
    }
}
