using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using TestApp.Mocking;
using Xunit;

namespace FakesUnitTests
{
   
    

    public class FakeNetworkStatusSuccess : IPing
    {
        public bool IsNetworkAvailable(string address) => true;
    }

    public class FakeNetworkStatusFailer : IPing
    {
        public bool IsNetworkAvailable(string address) => false;
    }

    public class ApiServiceTests
    {
        [Fact]
        public void Send_NetworkStatusSuccess_ShouldReturnPong()
        {
            // Arrange
            ApiService apiService = new ApiService(new FakeNetworkStatusSuccess());

            // Act
            var result = apiService.Send("1.1.1.1", "a");

            // Assert
            Assert.Equal("Pong", result);
        }

        [Fact]
        public void Send_NetworkStatusFailer_ShouldThrowNetworkException()
        {
            // Arrange
            ApiService apiService = new ApiService(new FakeNetworkStatusFailer());

            // Act
            Action act = () => apiService.Send("1.1.1.1", "a");

            // Assert
            Assert.Throws<NetworkInformationException>(act);
        }
    }
}
