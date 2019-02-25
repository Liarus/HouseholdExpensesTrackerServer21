using HouseholdExpensesTrackerServer21.Domain.Expenses.Events;
using HouseholdExpensesTrackerServer21.Domain.Expenses.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HouseholdExpensesTrackerServer21.Domain.Tests.Expenses
{
    [TestClass]
    public class ExpenseModelTests
    {
        [TestMethod]
        public void ShouldCreateNewModel()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            Guid expectedHouseholdId = Guid.NewGuid();
            Guid expectedExpenseTypeId = Guid.NewGuid();
            string expectedName = "Name";
            string expectedDescription = "Description";
            decimal expectedAmount = 102.12m;
            DateTime expectedDate = DateTime.Now;
            DateTime expectedStart = DateTime.Now;
            DateTime expectedEnd = DateTime.Now.AddYears(1);
            Period expectedPeriod = Period.Create(expectedStart, expectedEnd);

            // Act
            Expense actual = Expense.Create(expectedId, expectedHouseholdId, expectedExpenseTypeId,
                expectedName, expectedDescription, expectedAmount, expectedDate, expectedPeriod);

            // Assert
            Assert.IsTrue(actual != null);
            Assert.IsTrue(actual.Id == expectedId);
            Assert.IsTrue(actual.HouseholdId == expectedHouseholdId);
            Assert.IsTrue(actual.ExpenseTypeId == expectedExpenseTypeId);
            Assert.IsTrue(actual.Name == expectedName);
            Assert.IsTrue(actual.Description == expectedDescription);
            Assert.IsTrue(actual.Amount == expectedAmount);
            Assert.IsTrue(actual.Date == expectedDate);
            Assert.IsTrue(actual.Period != null);
            Assert.IsTrue(actual.Period.Equals(expectedPeriod));
            Assert.IsTrue(!string.IsNullOrEmpty(actual.SearchValue));
        }

        [TestMethod]
        public void ShouldHaveCreatedEvent()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            Guid expectedHouseholdId = Guid.NewGuid();
            Guid expectedExpenseTypeId = Guid.NewGuid();
            string expectedName = "Name";
            string expectedDescription = "Description";
            decimal expectedAmount = 102.12m;
            DateTime expectedDate = DateTime.Now;
            DateTime expectedStart = DateTime.Now;
            DateTime expectedEnd = DateTime.Now.AddYears(1);
            Period expectedPeriod = Period.Create(expectedStart, expectedEnd);

            // Act
            Expense actual = Expense.Create(expectedId, expectedHouseholdId, expectedExpenseTypeId,
                expectedName, expectedDescription, expectedAmount, expectedDate, expectedPeriod);

            // Assert
            Assert.IsTrue(actual != null);
            Assert.IsTrue(actual.Events != null);
            Assert.IsTrue(actual.Events.Count == 1);
            var events = actual.FlushUncommitedEvents();
            Assert.IsTrue(events != null);
            Assert.IsTrue(events.Length == 1);
            Assert.IsTrue(events[0] is ExpenseCreatedEvent);
            ExpenseCreatedEvent @event = events[0] as ExpenseCreatedEvent;
            Assert.IsTrue(@event.EntityId == expectedId);
            Assert.IsTrue(@event.HouseholdId == expectedHouseholdId);
            Assert.IsTrue(@event.ExpenseTypeId == expectedExpenseTypeId);
            Assert.IsTrue(@event.Description == expectedDescription);
            Assert.IsTrue(@event.Name == expectedName);
            Assert.IsTrue(@event.Date == expectedDate);
            Assert.IsTrue(@event.PeriodStart == expectedStart);
            Assert.IsTrue(@event.PeriodEnd == expectedEnd);
        }

        [TestMethod]
        public void ShouldModifyModel()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            Guid expectedHouseholdId = Guid.NewGuid();
            Guid expectedExpenseTypeId = Guid.NewGuid();
            string expectedName = "ModifiedName";
            string expectedDescription = "ModifiedDescription";
            decimal expectedAmount = 0.12m;
            DateTime expectedDate = DateTime.Now.AddDays(1);
            DateTime expectedStart = DateTime.Now.AddDays(1);
            DateTime expectedEnd = DateTime.Now.AddYears(2);
            Period expectedPeriod = Period.Create(expectedStart, expectedEnd);

            Expense actual = Expense.Create(expectedId, expectedHouseholdId, Guid.NewGuid(),
                "Name", "Description", 100.12M, DateTime.Now, Period.Create(DateTime.Now, DateTime.Now));

            // Act
            actual.Modify(expectedExpenseTypeId, expectedName, expectedDescription, expectedAmount,
                expectedDate, Period.Create(expectedStart, expectedEnd), 1);

            // Assert
            Assert.IsTrue(actual.Id == expectedId);
            Assert.IsTrue(actual.Name == expectedName);
            Assert.IsTrue(actual.Description == expectedDescription);
            Assert.IsTrue(actual.Date == expectedDate);
            Assert.IsTrue(actual.Amount == expectedAmount);
            Assert.IsTrue(actual.ExpenseTypeId == expectedExpenseTypeId);
            Assert.IsTrue(actual.Period != null);
            Assert.IsTrue(actual.Period.PeriodStart == expectedStart);
            Assert.IsTrue(actual.Period.PeriodEnd == expectedEnd);
            Assert.IsTrue(!string.IsNullOrEmpty(actual.SearchValue));
        }

        [TestMethod]
        public void ShouldHaveModifiedEvent()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();
            Guid expectedExpenseTypeId = Guid.NewGuid();
            string expectedName = "ModifiedName";
            string expectedDescription = "ModifiedDescription";
            decimal expectedAmount = 0.12m;
            DateTime expectedDate = DateTime.Now.AddDays(1);
            DateTime expectedStart = DateTime.Now.AddDays(1);
            DateTime expectedEnd = DateTime.Now.AddYears(2);

            Expense actual = Expense.Create(expectedId, Guid.NewGuid(), Guid.NewGuid(),
               "Name", "Description", 100.12M, DateTime.Now, Period.Create(DateTime.Now, DateTime.Now));
            actual.FlushUncommitedEvents();

            // Act
            actual.Modify(expectedExpenseTypeId, expectedName, expectedDescription, expectedAmount,
                expectedDate, Period.Create(expectedStart, expectedEnd), 1);

            // Assert
            Assert.IsTrue(actual.Events != null);
            Assert.IsTrue(actual.Events.Count == 1);
            var events = actual.FlushUncommitedEvents();
            Assert.IsTrue(events != null);
            Assert.IsTrue(events.Length == 1);
            Assert.IsTrue(events[0] is ExpenseModifiedEvent);
            ExpenseModifiedEvent @event = events[0] as ExpenseModifiedEvent;
            Assert.IsTrue(@event.EntityId == expectedId);
            Assert.IsTrue(@event.ExpenseTypeId == expectedExpenseTypeId);
            Assert.IsTrue(@event.Amount == expectedAmount);
            Assert.IsTrue(@event.Description == expectedDescription);
            Assert.IsTrue(@event.Name == expectedName);
            Assert.IsTrue(@event.Date == expectedDate);
            Assert.IsTrue(@event.PeriodStart == expectedStart);
            Assert.IsTrue(@event.PeriodEnd == expectedEnd);
        }

        [TestMethod]
        public void ShouldHaveDeletedEvent()
        {
            // Arrange
            Guid expectedId = Guid.NewGuid();

            Expense actual = Expense.Create(expectedId, Guid.NewGuid(), Guid.NewGuid(),
                "Name", "Description", 100.12M, DateTime.Now, Period.Create(DateTime.Now, DateTime.Now));
            actual.FlushUncommitedEvents();

            // Act
            actual.Delete();

            // Assert
            Assert.IsTrue(actual.Events != null);
            Assert.IsTrue(actual.Events.Count == 1);
            var events = actual.FlushUncommitedEvents();
            Assert.IsTrue(events != null);
            Assert.IsTrue(events.Length == 1);
            Assert.IsTrue(events[0] is ExpenseDeletedEvent);
            ExpenseDeletedEvent @event = events[0] as ExpenseDeletedEvent;
            Assert.IsTrue(@event.EntityId == expectedId);
        }
    }
}
