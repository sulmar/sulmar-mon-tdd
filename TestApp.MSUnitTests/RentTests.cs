using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.MSUnitTests
{
    [TestClass]
    public class RentTests
    {
        private Rent rent;

        private User rentee;

        [TestInitialize]
        public void Setup()
        {
            rentee = new User();
            rent = new Rent { Rentee = rentee };
        }

        [TestMethod]
        public void CanReturn_UserIsAdmin_ReturnsTrue()
        {
            // Arrange

            // Act
            var result = rent.CanReturn(new User { IsAdmin = true });

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanReturn_UserIsRentee_ReturnsTrue()
        {
            // Arrange

            // Act
            var result = rent.CanReturn(rentee);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanReturn_UserIsNotRentee_ReturnsFalse()
        {
            // Arrange

            // Act
            var result = rent.CanReturn(new User());

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CanReturn_UserIsEmpty_ThrowsArgumentNullException()
        {
            // Arrange

            // Act
            Action act = () => rent.CanReturn(null);

            // Assert
            Assert.ThrowsException<ArgumentNullException>(act);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CanReturn_UserIsEmpty_ThrowsArgumentNullExceptionWithAttribute()
        {
            // Arrange

            // Act
            var result = rent.CanReturn(null);

            // Assert
        }


    }
}
