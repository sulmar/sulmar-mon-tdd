using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Api.IntegrationTests
{

    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(async context => await context.Response.WriteAsync("Hello World!"));
        }
    }

    // dotnet add package Microsoft.AspNetCore.TestHost
    public class StartupTests
    {
        private TestServer server;
        private HttpClient client;

        public StartupTests()
        {
            server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .UseEnvironment("Development"));

            client = server.CreateClient();
                
        }


        [Fact]
        public async Task Get_Index_ShouldBeContentHelloWorld()
        {
            // Arrange

            // Act
            var result = await client.GetAsync("/");
            string content = await result.Content.ReadAsStringAsync();

            // Assert
            result.IsSuccessStatusCode.Should().BeTrue();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);

            content.Should().Be("Hello World!");
        }
    }
}
