using Moq;
using TestApp.Mocking;
using Xunit;
using FluentAssertions;

namespace TestApp.MocksUnitTests
{
    public class StandardShippingCostCalculatorLinqToMocksTests
    {
        private IShippingCostCalculator calculator;

        public StandardShippingCostCalculatorLinqToMocksTests()
        {
            calculator = new StandardShippingCostCalculator();
        }

        [Fact]
        public void CalculateShippingCost_BelowLimit_ShouldBeCostShipping()
        {
            // Arrange
            Order order = Mock.Of<Order>(o => o.Total == 0.01m);
            Mock<Order> mockOrder = Mock.Get(order);

            mockOrder
                .Setup(m => m.Total)
                .Verifiable();

            // Act
            var result = calculator.CalculateShippingCost(order);

            // Assert
            result.Should().BeGreaterThan(0);

            mockOrder.Verify(m => m.Total, Times.Once);
        }

        [Fact]
        public void CalculateShippingCost_AboveLimit_ShouldBeFreeShipping()
        {
            // Arrange
            Order order = Mock.Of<Order>(o => o.Total == 1000m);

            // Act
            var result = calculator.CalculateShippingCost(order);

            // Assert
            result.Should().Be(0);
        }
    }

    public class StandardShippingCostCalculatorTests
    {
        private IShippingCostCalculator calculator;
        private Mock<Order> mockOrder;
        private Order order;

        public StandardShippingCostCalculatorTests()
        {
            calculator = new StandardShippingCostCalculator();
            mockOrder = new Mock<Order>();
            order = mockOrder.Object;
        }

        [Fact]
        public void CalculateShippingCost_BelowLimit_ShouldBeCostShipping()
        {
            // Arrange
            mockOrder
                .SetupGet(o => o.Total)
                .Returns(0.01m);

            // Act
            var result = calculator.CalculateShippingCost(order);

            // Assert
            result.Should().BeGreaterThan(0);

        }

        [Fact]
        public void CalculateShippingCost_AboveLimit_ShouldBeFreeShipping()
        {
            // Arrange
            mockOrder
                .SetupGet(o => o.Total)
                .Returns(1000m);

            // Act
            var result = calculator.CalculateShippingCost(order);

            // Assert
            result.Should().Be(0);
        }
    }
}
