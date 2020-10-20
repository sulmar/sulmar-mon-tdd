using System;
using Xunit;
using FluentAssertions;

namespace TestApp.FluentAssertionsUnitTests
{

    // dotnet add package FluentAssertions
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
            result.Should().Be(3);
        }



        [Fact]
        public void Max_FirstAndSecondArgumentIsEqual_ReturnsTheSameArgument()
        {
            // Act
            var result = mathCalculator.Max(1, 1);

            // Assert
            result.Should().Be(1);

        }

        [Fact]
        public void Max_FirstArgumentIsGreater_ReturnsFirstArgument()
        {
            // Act
            var result = mathCalculator.Max(2, 1);

            // Assert
            result.Should().Be(2);
        }

        [Fact]
        public void Max_SecondArgumentIsGreater_ReturnsSecondArgument()
        {
            // Act
            var result = mathCalculator.Max(1, 2);

            // Assert
            result.Should().Be(2);
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
            result.Should().Be(expected);
        }
    }
}
