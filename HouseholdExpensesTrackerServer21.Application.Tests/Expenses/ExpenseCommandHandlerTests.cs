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
    public class ExpenseCommandHandlerTests
    {
        [TestMethod]
        public async Task ShouldCreateNew()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            var repository = new Mock<IExpenseRepository>();
            CreateExpenseCommand cmd =
                new CreateExpenseCommand(expectedId, Guid.NewGuid(), Guid.NewGuid(), "Name", "Description",
                0.12m, DateTime.Now, DateTime.Now, DateTime.Now);

            ExpenseCommandHandler actual = new ExpenseCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            repository.Verify(e =>
                e.Add(It.Is<Expense>(a => a.Id == expectedId)
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
            Expense entity = Expense.Create(expectedId, Guid.NewGuid(), Guid.NewGuid(), "Name", "Description",
                0.12m, DateTime.Now, Period.Create(DateTime.Now, DateTime.Now));
            var repository = new Mock<IExpenseRepository>();
            repository.Setup(
                e => e.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);
            ModifyExpenseCommand cmd =
                new ModifyExpenseCommand(expectedId, Guid.NewGuid(), expectedName, "Description",
                0.12m, DateTime.Now, DateTime.Now, DateTime.Now, 1);

            ExpenseCommandHandler actual = new ExpenseCommandHandler(repository.Object);

            // Act
            await actual.HandleAsync(cmd);

            // Assert
            Assert.IsTrue(entity.Id == expectedId);
            Assert.IsTrue(entity.Name == expectedName);
        }
    }
}
