using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestApp.XUnitTests
{
    public class MarkdownFormatterTests
    {
        private const string contentEncloseAsterix = @"^\*(\w|[[:blank:]])+\*$";


        [Fact]
        public void FormatAsBold_ValidContent_ShouldReturnsContentEncloseDoubleAsterix()
        {
            // Arrange
            MarkdownFormatter markdownFormatter = new MarkdownFormatter();

            // Act
            var result = markdownFormatter.FormatAsBold("abc");

            // Assert
            // Assert.Equal("**abc**", result);

            Assert.StartsWith("**", result);
            Assert.Contains("abc", result);
            Assert.EndsWith("**", result);
        }

        [Fact]
        public void FormatAsItalic_ValidContent_ShouldReturnsContentEncloseAsterix()
        {
            // Arrange
            MarkdownFormatter markdownFormatter = new MarkdownFormatter();

            // Act
            var result = markdownFormatter.FormatAsItalic("abc");

            // Assert
            // Assert.Equal("*abc*", result);

            //Assert.StartsWith("*", result);
            //Assert.Contains("abc", result);
            //Assert.EndsWith("*", result);

            Assert.Matches(contentEncloseAsterix, result);
        }


        [Fact]
        public void Validate_EmptyContent_ShouldThrowsFormatException()
        {
            // Arrange
            // Act
            Action act = () => MarkdownFormatter.Validate(string.Empty);

            // Assert
            Assert.Throws<FormatException>(act);
        }

        [Fact]
        public void Validate_NullContent_ShouldThrowsArgumentNullException()
        {
            // Arrange

            // Act
            Action act = () => MarkdownFormatter.Validate(null);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

    }
}
