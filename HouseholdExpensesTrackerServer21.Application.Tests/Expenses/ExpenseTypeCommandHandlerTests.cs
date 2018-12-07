using HouseholdExpensesTrackerServer21.Application.Expenses.Commands;
using HouseholdExpensesTrackerServer21.Application.Expenses.Handlers;
using HouseholdExpensesTrackerServer21.Domain.Expenses.Models;
using HouseholdExpensesTrackerServer21.Domain.Expenses.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HouseholdExpensesTrackerServer21.Application.Tests.Expenses
{
    [TestClass]
    public class ExpenseTypeCommandHandlerTests
    {
        [TestMethod]
        public async Task ShouldCreateNew()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            var repository = new Mock<IExpenseTypeRepository>();
            CreateExpenseTypeCommand cmd =
                new CreateExpenseTypeCommand(expectedId, Guid.NewGuid(), "Name", "Symbol");

            ExpenseTypeCommandHandler actual = new ExpenseTypeCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            repository.Verify(e =>
                e.Add(It.Is<ExpenseType>(a => a.Id == expectedId)
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
            ExpenseType entity = ExpenseType.Create(expectedId, Guid.NewGuid(), "Name", "Symbol");
            var repository = new Mock<IExpenseTypeRepository>();
            repository.Setup(e =>
                e.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);
            ModifyExpenseTypeCommand cmd =
                new ModifyExpenseTypeCommand(expectedId, expectedName, "Symbol", 1);

            ExpenseTypeCommandHandler actual = new ExpenseTypeCommandHandler(repository.Object);

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
            ExpenseType entity = ExpenseType.Create(Guid.NewGuid(), Guid.NewGuid(), "Name", "Symbol");
            var repository = new Mock<IExpenseTypeRepository>();
            repository.Setup(e =>
                e.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);
            DeleteExpenseTypeCommand cmd = new DeleteExpenseTypeCommand(entity.Id);

            ExpenseTypeCommandHandler actual = new ExpenseTypeCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            repository.Verify(e =>
                e.Delete(It.Is<ExpenseType>(a => a.Equals(entity))),
                Times.Once
            );
        }
    }
}
