using Moq;
using Newtonsoft.Json;
using System;
using TestApp.Mocking;
using Xunit;


// dotnet add package Moq
// dotnet add package FakeItEasy

namespace TestApp.MocksUnitTests
{
    public class TrackingServiceTests
    {
        private Mock<IFileReader> mockFileReader;
        private ITrackingService trackingService;

        public TrackingServiceTests()
        {
            mockFileReader = new Mock<IFileReader>();
            trackingService = new TrackingService(mockFileReader.Object);
        }


        [Fact]
        public void Get_ValidFile_ShouldReturnsLocation()
        {
            // Arrange
            // Mock<IFileReader> mockFileReader = new Mock<IFileReader>();

            mockFileReader
                .Setup(fr => fr.Get(It.IsAny<string>()))
                .Returns(JsonConvert.SerializeObject(new Location(52.05, 18.95)));

            //IFileReader fileReader = mockFileReader.Object;

            //ITrackingService trackingService = new TrackingService(fileReader);

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
            mockFileReader
                .Setup(fr => fr.Get(It.IsAny<string>()))
                .Returns("a");

            // Act
            Action act = () => trackingService.Get();

            // Assert
            Assert.Throws<FormatException>(act);

        }

        [Fact]
        public void Get_EmptyFile_ShouldThrowsApplicationException()
        {
            // Arrange
            mockFileReader
                .Setup(fr => fr.Get(It.IsAny<string>()))
                .Returns(string.Empty);

            // Act
            Action act = () => trackingService.Get();

            // Assert
            Assert.Throws<ApplicationException>(act);
        }
    }
}
