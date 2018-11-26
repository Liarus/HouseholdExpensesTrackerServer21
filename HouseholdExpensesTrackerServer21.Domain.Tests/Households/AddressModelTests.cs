using HouseholdExpensesTrackerServer21.Domain.Households.Events;
using HouseholdExpensesTrackerServer21.Domain.Households.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HouseholdExpensesTrackerServer21.Domain.Tests.Households
{
    [TestClass]
    public class AddressModelTests
    {
        [TestMethod]
        public void ShouldCreateNewModel()
        {
            // Arrange
            string expectedStreet = "Street";
            string expectedCity = "Katowice";
            string expectedZipCode = "43-000";
            string expectedCountry = "UK";

            // Act
            Address actual = Address.Create(expectedCountry, expectedZipCode,
                expectedCity, expectedStreet);
            // Assert
            Assert.IsTrue(actual != null);
            Assert.IsTrue(actual.City == expectedCity);
            Assert.IsTrue(actual.Country == expectedCountry);
            Assert.IsTrue(actual.Street == expectedStreet);
            Assert.IsTrue(actual.ZipCode == expectedZipCode);
        }
    }
}
