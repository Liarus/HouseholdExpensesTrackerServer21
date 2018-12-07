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
    public class SavingTypeCommandHandlerTests
    {
        [TestMethod]
        public async Task ShouldCreateNew()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            var repository = new Mock<ISavingTypeRepository>();
            CreateSavingTypeCommand cmd =
                new CreateSavingTypeCommand(expectedId, Guid.NewGuid(), "Name", "Symbol");

            SavingTypeCommandHandler actual = new SavingTypeCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            repository.Verify(e =>
                e.Add(It.Is<SavingType>(a => a.Id == expectedId)
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
            SavingType entity = SavingType.Create(expectedId, Guid.NewGuid(), "Name", "Symbol");
            var repository = new Mock<ISavingTypeRepository>();
            repository.Setup(e =>
                e.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);
            ModifySavingTypeCommand cmd =
                new ModifySavingTypeCommand(expectedId, expectedName, "Symbol", 1);

            SavingTypeCommandHandler actual = new SavingTypeCommandHandler(repository.Object);

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
            SavingType entity = SavingType.Create(Guid.NewGuid(), Guid.NewGuid(), "Name", "Symbol");
            var repository = new Mock<ISavingTypeRepository>();
            repository.Setup(e =>
                e.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);
            DeleteSavingTypeCommand cmd = new DeleteSavingTypeCommand(entity.Id);

            SavingTypeCommandHandler actual = new SavingTypeCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            repository.Verify(e =>
                e.Delete(It.Is<SavingType>(a => a.Equals(entity))),
                Times.Once
            );
        }
    }
}
