namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp;

using FluentAssertions;
using Generator.Models;
using Xunit;

public partial class CSharpProcessor_Should
{
    [Theory]
    [InlineData("vari_able", "VariAble")]
    [InlineData("va_ri_able", "VaRiAble")]
    public void ProcessPropertyNames(string variableName, string expected)
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
                        OriginalVariableNameToken = variableName,
                        IsProperty = true
                    }
                }
            }
        };

        // Act
        _sut.ProcessPropertiesByType(typeInfos);

        // Assert
        typeInfos.First().VariableInfos.First().VariableName.Should().Be(expected);
    }

    [Theory]
    [InlineData("vari_able", "VariAble", true)]
    [InlineData("vari_able", "VariAble", false)]
    public void ProcessPropertyNames_SkipsNonProperties(string variableName, string expected, bool isProperty)
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
                        OriginalVariableNameToken = variableName,
                        IsProperty = isProperty
                    }
                }
            }
        };

        // Act
        _sut.ProcessPropertiesByType(typeInfos);

        // Assert
        if (isProperty)
        {
            typeInfos.First().VariableInfos.First().VariableName.Should().Be(expected);
        }
        else
        {
            typeInfos.First().VariableInfos.First().VariableName.Should().BeNull();
        }
    }

    [Fact]
    public void ProcessPropertyNames_DoesNotThrowWhenTypeInfosEmpty()
    {
        // Arrange
        List<TypeInfo> typeInfos = new List<TypeInfo>
        {
        };

        // Act
        Action action = () => { _sut.ProcessPropertiesByType(typeInfos); };

        // Assert
        action.Should().NotThrow();
    }

    [Fact]
    public void ProcessPropertyNames_DoesNotThrowWhenVariableInfosEmpty()
    {
        // Arrange
        List<TypeInfo> typeInfos = new List<TypeInfo>
        {
            new TypeInfo
            {
            }
        };

        // Act
        Action action = () => { _sut.ProcessPropertiesByType(typeInfos); };

        // Assert
        action.Should().NotThrow();
    }
}