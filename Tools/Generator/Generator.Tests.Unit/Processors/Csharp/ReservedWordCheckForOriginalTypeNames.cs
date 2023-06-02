namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp;

using FluentAssertions;
using Generator;
using Generator.Models;
using Xunit;

public partial class CSharpProcessor_Should
{
    [Theory]
    [InlineData("asd", "asd", true)]
    [InlineData("Asd", "asd", true)]
    [InlineData("ASD", "asd", true)]
    [InlineData("asdasd", "asd", false)]
    [InlineData(null, "asd", false)]
    [InlineData("", "asd", false)]
    [InlineData(" ", "asd", false)]
    [InlineData("asdasd", null, false)]
    [InlineData("asdasd", "", false)]
    [InlineData("asdasd", " ", false)]
    public void ReservedWordCheckForOriginalTypeNames(string word, string reservedWord, bool shouldThrow)
    {
        // Arrange
        List<TypeInfo> typeInfos = new List<TypeInfo>
        {
            new TypeInfo { OriginalTypeNameToken = word }
        };

        List<string> reservedWords = new List<string> { reservedWord };

        // Act
        Action action = () => { _sut.ReservedWordCheckForOriginalTypeNames(typeInfos, reservedWords); };

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