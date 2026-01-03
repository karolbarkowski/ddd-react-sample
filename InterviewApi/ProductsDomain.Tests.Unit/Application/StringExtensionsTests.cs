using FluentAssertions;
using ProductsDomain.Application.Mappings;

namespace ProductsDomain.Tests.Unit.Application;

public class StringExtensionsTests
{
    [Fact]
    public void Capitalize_WithSingleCharacterLowercase_ShouldReturnUppercase()
    {
        // Arrange
        var input = "a";

        // Act
        var result = input.Capitalize();

        // Assert
        result.Should().Be("A");
    }

    [Fact]
    public void Capitalize_WithSingleCharacterUppercase_ShouldReturnUppercase()
    {
        // Arrange
        var input = "A";

        // Act
        var result = input.Capitalize();

        // Assert
        result.Should().Be("A");
    }

    [Fact]
    public void Capitalize_WithMultipleCharactersLowercase_ShouldCapitalizeFirstChar()
    {
        // Arrange
        var input = "hello";

        // Act
        var result = input.Capitalize();

        // Assert
        result.Should().Be("Hello");
    }

    [Fact]
    public void Capitalize_WithAlreadyCapitalized_ShouldReturnSame()
    {
        // Arrange
        var input = "Hello";

        // Act
        var result = input.Capitalize();

        // Assert
        result.Should().Be("Hello");
    }

    [Fact]
    public void Capitalize_WithEmptyString_ShouldReturnEmptyString()
    {
        // Arrange
        var input = "";

        // Act
        var result = input.Capitalize();

        // Assert
        result.Should().Be("");
    }

    [Fact]
    public void Capitalize_WithNull_ShouldReturnNull()
    {
        // Arrange
        string? input = null;

        // Act
        var result = input!.Capitalize();

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void Capitalize_WithWhitespace_ShouldReturnWhitespace()
    {
        // Arrange
        var input = "   ";

        // Act
        var result = input.Capitalize();

        // Assert
        result.Should().Be("   ");
    }
}
