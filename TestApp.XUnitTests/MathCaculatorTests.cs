using System;
using Xunit;

namespace TestApp.XUnitTests
{
    public class MathCaculatorTests
    {
        private MathCalculator mathCalculator;

        public MathCaculatorTests()
        {
            mathCalculator = new MathCalculator();
        }


        [Fact]
        public void Add_WhenCalled_ReturnsTheSumOfArguments()
        {
            // Act
            var result = mathCalculator.Add(1, 2);

            // Assert
            Assert.Equal(3, result);
        }

       

        [Fact]
        public void Max_FirstAndSecondArgumentIsEqual_ReturnsTheSameArgument()
        {
            // Act
            var result = mathCalculator.Max(1, 1);

            // Assert
            Assert.Equal(1, result);

        }

        [Fact]
        public void Max_FirstArgumentIsGreater_ReturnsFirstArgument()
        {
            // Act
            var result = mathCalculator.Max(2, 1);

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void Max_SecondArgumentIsGreater_ReturnsSecondArgument()
        {
            // Act
            var result = mathCalculator.Max(1, 2);

            // Assert
            Assert.Equal(2, result);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(2, 1, 2)]
        [InlineData(1, 2, 2)]
        public void Max_ValidArguments_ReturnsValidArgument(int first, int second, int expected)
        {
            // Act
            var result = mathCalculator.Max(first, second);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
