using Newtonsoft.Json;
using System;
using TestApp.Mocking;
using Xunit;

namespace FakesUnitTests
{
    public class FakeValidFile : IFileReader
    {
        public string Get(string filename)
        {
            Location location = new Location(52.05, 18.95);

            return JsonConvert.SerializeObject(location);
        }
    }

    public class FakeInvalidFile : IFileReader
    {
        public string Get(string filename) => "a";
    }

    public class FakeEmptyFile : IFileReader
    {
        public string Get(string filename) => string.Empty;
    }

    public class TrackingServiceTests
    {
        [Fact]
        public void Get_ValidFile_ShouldReturnsLocation()
        {
            // Arrange 
            IFileReader fileReader = new FakeValidFile();
            ITrackingService trackingService = new TrackingService(fileReader);

            // Act
            var result = trackingService.Get();

            // Assert
            Assert.Equal(52.05, result.Latitude);
            Assert.Equal(18.95, result.Longitude);


        }


        [Fact]
        public void Get_InvalidFile_ShouldThrowsFormatException()
        {
            // Arrange 
            IFileReader fileReader = new FakeInvalidFile();
            ITrackingService trackingService = new TrackingService(fileReader);

            // Act
            Action act = ()=>trackingService.Get();

            // Assert
            Assert.Throws<FormatException>(act);
        }

        [Fact]
        public void Get_EmptyFile_ShouldThrowsApplicationException()
        {
            // Arrange 
            IFileReader fileReader = new FakeEmptyFile();
            ITrackingService trackingService = new TrackingService(fileReader);

            // Act
            Action act = () => trackingService.Get();

            // Assert
            Assert.Throws<ApplicationException>(act);
        }
    }
}
