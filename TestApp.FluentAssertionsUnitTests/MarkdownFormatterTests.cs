using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using FluentAssertions.Primitives;

namespace TestApp.FluentAssertionsUnitTests
{

    public class Customer
    {

    }

    public class CustomerAssertions
    {
        private readonly Customer customer;

        public CustomerAssertions(Customer customer)
        {
            this.customer = customer;
        }

        [CustomAssertion]
        public void Validate()
        {
            customer.Should().NotBe(customer);
        }
    }

    public static class StringAssertionsExtensions
    {
        private const string contentEncloseAsterix = @"^\*(\w|[[:blank:]])+\*$";

        public static AndConstraint<StringAssertions> HasEncloseAsterix(this StringAssertions assertions)
        {
            return assertions.MatchRegex(contentEncloseAsterix);
        }
    }

    public class MarkdownFormatterTests
    {
        [Fact]
        public void FormatAsBold_ValidContent_ShouldReturnsContentEncloseDoubleAsterix()
        {
            // Arrange
            MarkdownFormatter markdownFormatter = new MarkdownFormatter();

            // Act
            var result = markdownFormatter.FormatAsBold("abc");

            // Assert
            // Assert.Equal("**abc**", result);

            //Assert.StartsWith("**", result);
            //Assert.Contains("abc", result);
            //Assert.EndsWith("**", result);

            result.Should()
                .StartWith("**")
                .And
                .Contain("abc")
                .And
                .EndWith("**");

        }

        [Fact]
        public void FormatAsItalic_ValidContent_ShouldHasContentEncloseAsterix()
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

            // Assert.Matches(contentEncloseAsterix, result);

            // result.Should().MatchRegex(contentEncloseAsterix);

            result.Should().HasEncloseAsterix();

        }


        [Fact]
        public void Validate_EmptyContent_ShouldThrowsFormatException()
        {
            // Arrange
            // Act
            Action act = () => MarkdownFormatter.Validate(string.Empty);

            // Assert
            // Assert.Throws<FormatException>(act);

            act.Should().ThrowExactly<FormatException>();
        }

        [Fact]
        public void Validate_NullContent_ShouldThrowsArgumentNullException()
        {
            // Arrange

            // Act
            Action act = () => MarkdownFormatter.Validate(null);

            // Assert
            // Assert.Throws<ArgumentNullException>(act);

            act.Should().ThrowExactly<ArgumentNullException>();
        }

    }
}
