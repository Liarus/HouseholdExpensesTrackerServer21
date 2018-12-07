using HouseholdExpensesTrackerServer21.Application.Households.Commands;
using HouseholdExpensesTrackerServer21.Application.Households.Handlers;
using HouseholdExpensesTrackerServer21.Domain.Households.Models;
using HouseholdExpensesTrackerServer21.Domain.Households.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HouseholdExpensesTrackerServer21.Application.Tests.Households
{
    [TestClass]
    public class HouseholdCommnadHandlerTests
    {
        [TestMethod]
        public async Task ShouldCreateNew()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            var repository = new Mock<IHouseholdRepository>();
            CreateHouseholdCommand cmd =
                new CreateHouseholdCommand(expectedId, Guid.NewGuid(), "Name", "Symbol", "Description",
                    "Street", "City", "Country", "00-000");

            HouseholdCommandHandler actual = new HouseholdCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            repository.Verify(e => 
                e.Add(It.Is<Household>(a => a.Id == expectedId)
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
            Household entity = Household.Create(expectedId, Guid.NewGuid(), "Name", "Symbol",
                "Description", Address.Create("Country", "00-000", "City", "Street"));
            var repository = new Mock<IHouseholdRepository>();
            repository.Setup(e =>
                e.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);
            ModifyHouseholdCommand cmd =
                new ModifyHouseholdCommand(expectedId, expectedName, "Symbol", "Description", "Street",
                    "City", "Country", "00-000", 1);

            HouseholdCommandHandler actual = new HouseholdCommandHandler(repository.Object);

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
            Household entity = Household.Create(Guid.NewGuid(), Guid.NewGuid(), "Name", "Symbol",
                "Description", Address.Create("Country", "00-000", "City", "Street"));
            var repository = new Mock<IHouseholdRepository>();
            repository.Setup(e =>
                e.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);
            DeleteHouseholdCommand cmd = new DeleteHouseholdCommand(entity.Id);

            HouseholdCommandHandler actual = new HouseholdCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            repository.Verify(e =>
                e.Delete(It.Is<Household>(a => a.Equals(entity))),
                Times.Once
            );
        }
    }
}
