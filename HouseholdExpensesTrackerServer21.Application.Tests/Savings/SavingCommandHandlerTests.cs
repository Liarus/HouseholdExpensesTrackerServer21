using HouseholdExpensesTrackerServer21.Application.Savings.Commands;
using HouseholdExpensesTrackerServer21.Application.Savings.Handlers;
using HouseholdExpensesTrackerServer21.Domain.Savings.Models;
using HouseholdExpensesTrackerServer21.Domain.Savings.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HouseholdSavingsTrackerServer21.Application.Tests.Savings
{
    [TestClass]
    public class SavingCommandHandlerTests
    {
        [TestMethod]
        public async Task ShouldCreateNew()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            var repository = new Mock<ISavingRepository>();
            CreateSavingCommand cmd =
                new CreateSavingCommand(expectedId, Guid.NewGuid(), Guid.NewGuid(), "Name", "Description",
                0.12m, DateTime.Now);

            SavingCommandHandler actual = new SavingCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            repository.Verify(e =>
                e.Add(It.Is<Saving>(a => a.Id == expectedId)
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
            Saving entity = Saving.Create(expectedId, Guid.NewGuid(), Guid.NewGuid(), "Name", "Description",
                0.12m, DateTime.Now);
            var repository = new Mock<ISavingRepository>();
            repository.Setup(
                e => e.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);
            ModifySavingCommand cmd =
                new ModifySavingCommand(expectedId, Guid.NewGuid(), expectedName, "Description",
                0.12m, DateTime.Now, 1);

            SavingCommandHandler actual = new SavingCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            Assert.IsTrue(entity.Id == expectedId);
            Assert.IsTrue(entity.Name == expectedName);
        }
    }
}
