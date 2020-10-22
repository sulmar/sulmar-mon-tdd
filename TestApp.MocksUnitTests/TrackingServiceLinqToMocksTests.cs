using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Mocking;
using Xunit;

namespace TestApp.MocksUnitTests
{

    // Linq To Moq
    // zapis deklaratywny

    public class TrackingServiceLinqToMocksTests
    {
        private IFileReader fileReader;

        // private ITrackingService trackingService;
        private Lazy<ITrackingService> lazyTackingService;
        private ITrackingService trackingService => lazyTackingService.Value;

        public TrackingServiceLinqToMocksTests()
        {
            // trackingService = new TrackingService(fileReader);

            lazyTackingService = new Lazy<ITrackingService>(()=> new TrackingService(fileReader));
        }

        [Fact]
        public void Get_ValidFile_ShouldReturnsLocation()
        {
            //mockFileReader
            //   .Setup(fr => fr.Get(It.IsAny<string>()))
            //   .Returns(JsonConvert.SerializeObject(new Location(52.05, 18.95)));

            // Arrange
            fileReader = Mock.Of<IFileReader>(
                fr => fr.Get(It.IsAny<string>()) == JsonConvert.SerializeObject(new Location(52.05, 18.95)));

            // Act
            var result = trackingService.Get();

            // Assert
            result.Latitude.Should().Be(52.05);
            result.Longitude.Should().Be(18.95);

        }

        [Fact]
        public void Get_InvalidFile_ShouldThrowsFormatException()
        {
            // Arrange
            fileReader = Mock.Of<IFileReader>(
                fr => fr.Get(It.IsAny<string>()) == "a");

            // Act
            Action act = () => trackingService.Get();

            // Assert
            act.Should().ThrowExactly<FormatException>();
        }

        [Fact]
        public void Get_EmptyFile_ShouldThrowsApplicationException()
        {
            // Arrange
            fileReader = Mock.Of<IFileReader>(
                fr => fr.Get(It.IsAny<string>()) == string.Empty);

            // Act
            Action act = () => trackingService.Get();

            // Assert
            act.Should().ThrowExactly<ApplicationException>();
        }
    }

}
