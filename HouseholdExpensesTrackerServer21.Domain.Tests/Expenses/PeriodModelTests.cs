using HouseholdExpensesTrackerServer21.Domain.Expenses.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HouseholdExpensesTrackerServer21.Domain.Tests.Expenses
{
    [TestClass]
    public class PeriodModelTests
    {
        [TestMethod]
        public void ShouldCreateNewModel()
        {
            // Arrange
            DateTime expectedStart = DateTime.Now;
            DateTime expectedEnd = DateTime.Now.AddYears(1);

            // Act
            Period actual = Period.Create(expectedStart, expectedEnd);

            // Assert
            Assert.IsTrue(actual != null);
            Assert.IsTrue(actual.PeriodStart == expectedStart);
            Assert.IsTrue(actual.PeriodEnd == expectedEnd);
        }
    }
}
