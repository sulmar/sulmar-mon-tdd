using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;

namespace Api.IntegrationTests
{
    // dotnet add package Microsoft.AspNetCore.Mvc.Testing
    public class VehiclesControllerFixtureTests : IClassFixture<WebApplicationFactory<WebApi.Startup>>
    {
        private WebApplicationFactory<WebApi.Startup> factory;

        public VehiclesControllerFixtureTests(WebApplicationFactory<WebApi.Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task GetById_ExistsId_ShouldReturnVehicle()
        {
            // Arrange

            var client = factory.CreateClient();

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
            var client = factory.CreateClient();

            // Act
            var result = await client.GetAsync("api/vehicles/0");

            // Assert
            result.IsSuccessStatusCode.Should().BeFalse();
            result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }


    }
}
