namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp;

using FluentAssertions;
using Generator;
using Generator.Models;
using Xunit;

public partial class CSharpProcessor_Should
{
    [Theory]
    [InlineData("reserved", "reserved")]
    [InlineData("REserved", "reserved")]
    [InlineData("REserveD", "reserved")]
    [InlineData(" REserveD", "reserved")]
    [InlineData("REserveD ", "reserved")]
    [InlineData(" REserveD ", "reserved")]
    public void ReservedWordsCheckForOriginalVariableNamesOfAType(
        string tokenName,
        string reservedWord)
    {
        // Arrange
        List<TypeInfo> typeInfos = new List<TypeInfo>
        {
            new TypeInfo
            {
                VariableInfos = new List<VariableInfo>
                {
                    new VariableInfo
                    {
                        OriginalVariableNameToken = tokenName
                    }
                }
            }
        };
        List<string> reservedWords = new List<string> { reservedWord };

        // Act
        Action action = () => { _sut.ReservedWordsCheckForOriginalVariableNamesOfAType(typeInfos, reservedWords); };

        // Assert
        action.Should().Throw<GeneratorException>();
    }

    [Theory]
    [InlineData(null, "reserved")]
    [InlineData("", "reserved")]
    [InlineData(" ", "reserved")]
    public void ReservedWordsCheckForOriginalVariableNamesOfAType_DoesNotThrow_WhenTokenIsNullOrEmpty(
        string tokenName,
        string reservedWord)
    {
        // Arrange
        List<TypeInfo> typeInfos = new List<TypeInfo>
        {
            new TypeInfo
            {
                VariableInfos = new List<VariableInfo>
                {
                    new VariableInfo
                    {
                        OriginalVariableNameToken = tokenName
                    }
                }
            }
        };
        List<string> reservedWords = new List<string> { reservedWord };

        // Act
        Action action = () => { _sut.ReservedWordsCheckForOriginalVariableNamesOfAType(typeInfos, reservedWords); };

        // Assert
        action.Should().NotThrow<GeneratorException>();
    }

    [Theory]
    [InlineData("asd", "reserved")]
    [InlineData("bsd", "reserved")]
    [InlineData("csd", "reserved")]
    public void ReservedWordsCheckForOriginalVariableNamesOfAType_DoesNotThrow(
        string tokenName,
        string reservedWord)
    {
        // Arrange
        List<TypeInfo> typeInfos = new List<TypeInfo>
        {
            new TypeInfo
            {
                VariableInfos = new List<VariableInfo>
                {
                    new VariableInfo
                    {
                        OriginalVariableNameToken = tokenName
                    }
                }
            }
        };
        List<string> reservedWords = new List<string> { reservedWord };

        // Act
        Action action = () => { _sut.ReservedWordsCheckForOriginalVariableNamesOfAType(typeInfos, reservedWords); };

        // Assert
        action.Should().NotThrow<GeneratorException>();
    }

    [Fact]
    public void ReservedWordsCheckForOriginalVariableNamesOfAType_DoesNotThrow_WhenReservedListEmpty()
    {
        // Arrange
        List<TypeInfo> typeInfos = new List<TypeInfo>
        {
            new TypeInfo
            {
                VariableInfos = new List<VariableInfo>
                {
                    new VariableInfo
                    {
                        OriginalVariableNameToken = "tokenName"
                    }
                }
            }
        };
        List<string> reservedWords = new List<string> { };

        // Act
        Action action = () => { _sut.ReservedWordsCheckForOriginalVariableNamesOfAType(typeInfos, reservedWords); };

        // Assert
        action.Should().NotThrow<GeneratorException>();
    }

    [Fact]
    public void ReservedWordsCheckForOriginalVariableNamesOfAType_DoesNotThrow_WhenTypeInfosListEmpty()
    {
        // Arrange
        List<TypeInfo> typeInfos = new List<TypeInfo>
        {
            new TypeInfo
            {
            }
        };
        List<string> reservedWords = new List<string> { "single" };

        // Act
        Action action = () => { _sut.ReservedWordsCheckForOriginalVariableNamesOfAType(typeInfos, reservedWords); };

        // Assert
        action.Should().NotThrow<GeneratorException>();
    }
}