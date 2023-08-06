namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp;

using FluentAssertions;
using Generator;
using Generator.Models;
using Xunit;

public partial class CSharpProcessor_Should
{
    // [Theory]
    [InlineData("asd", "asd", true)]
    [InlineData("Asd", "asd", true)]
    [InlineData("ASD", "asd", true)]
    [InlineData("asdd", "asd", false)]
    [InlineData(null, "asd", false)]
    [InlineData("", "asd", false)]
    [InlineData(" ", "asd", false)]
    [InlineData("asdd", null, false)]
    [InlineData("asdd", "", false)]
    [InlineData("asdd", " ", false)]
    public void ReservedWordCheckForOriginalDtoNamespaceToken(
        string input,
        string reservedWord,
        bool shouldThrow)
    {
        // Arrange
        List<TypeInfo> typeInfos = new List<TypeInfo>
        {
            new TypeInfo { OriginalDtoNamespaceToken = input }
        };
        List<string> reservedWords = new List<string> { reservedWord };

        // Act
        Action action = () => { _sut.ReservedWordCheckForOriginalDtoNamespaceToken(typeInfos, reservedWords); };

        // Assert
        if (shouldThrow)
        {
            action.Should().Throw<GeneratorException>();
        }
        else
        {
            action.Should().NotThrow();
        }
    }
}