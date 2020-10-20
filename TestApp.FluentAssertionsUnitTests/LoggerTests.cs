using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestApp.FluentAssertionsUnitTests
{
    public class LoggerTests
    {
        private TimeSpan limit = TimeSpan.FromMilliseconds(100);

        [Fact]
        public void Log_WhenCalled_ShouldBeExecutionTimeBelowLimit()
        {
            // Arrange
            Logger logger = new Logger();

            // Act
            Action act = () => logger.Log("a");

            // Assert
            act.ExecutionTime().Should().BeLessOrEqualTo(limit);
        }
    }
}
