using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Fundamentals.Gus;
using Xunit;

namespace TestApp.XUnitTests
{
    public class ReportFactoryTests
    {
        [Theory]
        [InlineData("P")]
        [InlineData("LP")]
        [InlineData("LF")]
        public void Create_TypeIsPOrLPOrLF_ReturnsReturnsLegalPersonalityReport(string type)
        {
            // Arrange

            // Act
            var result = ReportFactory.Create(type);

            // Assert
            Assert.IsType<LegalPersonality>(result);
            Assert.IsAssignableFrom<Report>(result);
        }


        [Fact]
        public void Create_TypeIsF_ReturnsSoleTraderReport()
        {
            // Arrange

            // Act
            var result = ReportFactory.Create("F");

            // Assert
            Assert.IsType<SoleTraderReport>(result);
            Assert.IsAssignableFrom<Report>(result);

        }
    }
}
