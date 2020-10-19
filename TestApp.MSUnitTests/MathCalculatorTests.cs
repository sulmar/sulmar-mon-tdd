using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestApp.MSUnitTests
{
    [TestClass]
    public class MathCalculatorTests
    {

        // Method_Scenario_ExpectedBehiavior


        private MathCalculator mathCalculator;

        [TestInitialize]
        public void Setup()
        {
            // Arrange
            mathCalculator = new MathCalculator();
        }

        [TestMethod]
        public void Add_WhenCalled_ReturnsTheSumOfArguments()
        {
            // Act
            var result = mathCalculator.Add(1, 2);

            // Assert
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Max_FirstAndSecondArgumentIsEqual_ReturnsTheSameArgument()
        {
            // Act
            var result = mathCalculator.Max(1, 1);

            // Assert
            Assert.AreEqual(1, result);

        }

        [TestMethod]
        public void Max_FirstArgumentIsGreater_ReturnsFirstArgument()
        {
            // Act
            var result = mathCalculator.Max(2, 1);

            // Assert
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Max_SecondArgumentIsGreater_ReturnsSecondArgument()
        {
            // Act
            var result = mathCalculator.Max(1, 2);

            // Assert
            Assert.AreEqual(2, result);
        }

        [DataRow(1, 1, 1, DisplayName = "FirstAndSecondArgumentIsEqual")]
        [DataRow(2, 1, 2, DisplayName = "FirstArgumentIsGreater")]
        [DataRow(1, 2, 2, DisplayName = "SecondArgumentIsGreater")]
        [DataTestMethod]
        public void Max_ValidArguments_ReturnsValidArgument(int first, int second, int expected)
        {
            // Act
            var result = mathCalculator.Max(first, second);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
