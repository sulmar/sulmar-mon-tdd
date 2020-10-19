using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestApp.XUnitTests
{
    public class RentTests
    {
        private Rent rent;

        private User rentee;

        public RentTests()
        {
            rentee = new User();
            rent = new Rent { Rentee = rentee };
        }

        [Fact]
        public void CanReturn_UserIsAdmin_ReturnsTrue()
        {
            // Arrange

            // Act
            var result = rent.CanReturn(new User { IsAdmin = true });

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CanReturn_UserIsRentee_ReturnsTrue()
        {
            // Arrange

            // Act
            var result = rent.CanReturn(rentee);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CanReturn_UserIsNotRentee_ReturnsFalse()
        {
            // Arrange

            // Act
            var result = rent.CanReturn(new User());

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CanReturn_UserIsEmpty_ThrowsArgumentNullException()
        {
            // Arrange

            // Act
            Action act = () => rent.CanReturn(null);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }
    }
}
