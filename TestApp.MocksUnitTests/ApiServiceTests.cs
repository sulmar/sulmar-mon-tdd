using Moq;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using TestApp.Mocking;
using Xunit;

namespace TestApp.MocksUnitTests
{

    public class ApiServiceTests
    {
        private Mock<IPing> mockPing;
        private ApiService apiService;
        public ApiServiceTests()
        {
            mockPing = new Mock<IPing>();
            apiService = new ApiService(mockPing.Object);
        }

        [Fact]
        public void Send_NetworkStatusSuccess_ShouldReturnPong()
        {
            // Arrange
            mockPing
                .Setup(p => p.IsNetworkAvailable(It.IsAny<string>()))
                .Returns(true);

            // Act
            var result = apiService.Send("1.1.1.1", "a");

            // Assert
            Assert.Equal("Pong", result);
        }

        [Fact]
        public void Send_NetworkStatusFailer_ShouldThrowNetworkException()
        {
            // Arrange
            mockPing
              .Setup(p => p.IsNetworkAvailable(It.IsAny<string>()))
              .Returns(false);

            // Act
            Action act = () => apiService.Send("1.1.1.1", "a");

            // Assert
            Assert.Throws<NetworkInformationException>(act);
        }
    }
}
