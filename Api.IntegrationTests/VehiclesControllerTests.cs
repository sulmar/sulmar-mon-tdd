using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.IntegrationTests
{
    public class VehiclesControllerTests
    {
        private TestServer server;
        private HttpClient client;

        public VehiclesControllerTests()
        {
            server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<WebApi.Startup>()
                .UseEnvironment("Development"));

            client = server.CreateClient();
        }

        [Fact]
        public async Task GetById_ExistsId_ShouldReturnVehicle()
        {
            // Arrange

            // Act
            var result = await client.GetAsync("api/vehicles/1");

            // Assert
            result.IsSuccessStatusCode.Should().BeTrue();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task GetById_NotExistsId_ShouldNotReturnVehicle()
        {
            // Arrange

            // Act
            var result = await client.GetAsync("api/vehicles/0");

            // Assert
            result.IsSuccessStatusCode.Should().BeFalse();
            result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }



    }
}
