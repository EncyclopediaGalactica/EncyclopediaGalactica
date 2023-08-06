namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp;

using FluentAssertions;
using Generator.Models;
using Xunit;

public partial class CSharpProcessor_Should
{
    [Theory]
    [InlineData("typename", true, true)]
    [InlineData("typename", false, false)]
    [InlineData(null, false, false)]
    [InlineData("", false, false)]
    [InlineData(" ", false, false)]
    [InlineData(null, true, false)]
    [InlineData("", true, false)]
    [InlineData(" ", true, false)]
    public void ProcessNullableVariableTypes(string typeName, bool isNullable, bool expected)
    {
        // Arrange
        List<TypeInfo> fileInfos = new()
        {
            new TypeInfo
            {
                VariableInfos = new List<VariableInfo>
                {
                    new()
                    {
                        OriginalVariableNameToken = typeName,
                    }
                },
            }
        };

        if (!isNullable)
        {
            fileInfos[0].RequiredProperties = new List<string> { typeName };
        }

        // Act
        _sut.ProcessNullableVariableTypes(fileInfos);

        // Assert
        fileInfos[0].VariableInfos.ToList()[0].IsNullable.Should().Be(expected);
    }
}