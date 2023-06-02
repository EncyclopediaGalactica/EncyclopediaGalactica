namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Processors.Csharp;

using FluentAssertions;
using Generator;
using Generator.Generators;
using Generator.Models;
using Xunit;

public partial class CSharpProcessor_Should
{
    [Theory]
    [InlineData("integer", "int32", "int")]
    [InlineData("integer", "int64", "long")]
    [InlineData("number", "float", "float")]
    [InlineData("number", "double", "double")]
    [InlineData("string", null, "string")]
    [InlineData("string", "", "string")]
    [InlineData("string", " ", "string")]
    [InlineData("string", "byte", "string")]
    [InlineData("string", "binary", "string")]
    [InlineData("string", "date", "string")]
    [InlineData("string", "date-time", "string")]
    [InlineData("boolean", null, "bool")]
    [InlineData("boolean", "", "bool")]
    [InlineData("boolean", " ", "bool")]
    [InlineData(null, " ", "")]
    [InlineData("", " ", "")]
    [InlineData(" ", " ", "")]
    public void ProcessOpenApiTypesToCsharpTypes(
        string typeNameToken,
        string format,
        string expected)
    {
        // Arrange
        List<TypeInfo> fileInfos = new List<TypeInfo>
        {
            new TypeInfo
            {
                VariableInfos = new List<VariableInfo>
                {
                    new VariableInfo
                    {
                        OriginalVariableTypeNameToken = typeNameToken,
                        OriginalVariableTypeFormat = format
                    }
                }
            }
        };
        CSharpGenerator cSharpGenerator = new CSharpGenerator();

        // Act
        _sut.ProcessOpenApiTypesToCsharpTypes(fileInfos, cSharpGenerator.OpenApiCsharpTypeMap);

        // Assert
        fileInfos[0].VariableInfos.ToList()[0].VariableTypeName.Should().Be(expected);
        if (expected == "string")
        {
            fileInfos[0].VariableInfos.ToList()[0].IsString.Should().BeTrue();

            fileInfos[0].VariableInfos.ToList()[0].IsInt.Should().BeFalse();
            fileInfos[0].VariableInfos.ToList()[0].IsLong.Should().BeFalse();
            fileInfos[0].VariableInfos.ToList()[0].IsFloat.Should().BeFalse();
            fileInfos[0].VariableInfos.ToList()[0].IsDouble.Should().BeFalse();
            fileInfos[0].VariableInfos.ToList()[0].IsBool.Should().BeFalse();
        }

        if (expected == "int")
        {
            fileInfos[0].VariableInfos.ToList()[0].IsInt.Should().BeTrue();

            fileInfos[0].VariableInfos.ToList()[0].IsString.Should().BeFalse();
            fileInfos[0].VariableInfos.ToList()[0].IsLong.Should().BeFalse();
            fileInfos[0].VariableInfos.ToList()[0].IsFloat.Should().BeFalse();
            fileInfos[0].VariableInfos.ToList()[0].IsDouble.Should().BeFalse();
            fileInfos[0].VariableInfos.ToList()[0].IsBool.Should().BeFalse();
        }

        if (expected == "long")
        {
            fileInfos[0].VariableInfos.ToList()[0].IsLong.Should().BeTrue();

            fileInfos[0].VariableInfos.ToList()[0].IsString.Should().BeFalse();
            fileInfos[0].VariableInfos.ToList()[0].IsInt.Should().BeFalse();
            fileInfos[0].VariableInfos.ToList()[0].IsFloat.Should().BeFalse();
            fileInfos[0].VariableInfos.ToList()[0].IsDouble.Should().BeFalse();
            fileInfos[0].VariableInfos.ToList()[0].IsBool.Should().BeFalse();
        }

        if (expected == "float")
        {
            fileInfos[0].VariableInfos.ToList()[0].IsFloat.Should().BeTrue();

            fileInfos[0].VariableInfos.ToList()[0].IsString.Should().BeFalse();
            fileInfos[0].VariableInfos.ToList()[0].IsInt.Should().BeFalse();
            fileInfos[0].VariableInfos.ToList()[0].IsLong.Should().BeFalse();
            fileInfos[0].VariableInfos.ToList()[0].IsDouble.Should().BeFalse();
            fileInfos[0].VariableInfos.ToList()[0].IsBool.Should().BeFalse();
        }

        if (expected == "double")
        {
            fileInfos[0].VariableInfos.ToList()[0].IsDouble.Should().BeTrue();

            fileInfos[0].VariableInfos.ToList()[0].IsString.Should().BeFalse();
            fileInfos[0].VariableInfos.ToList()[0].IsInt.Should().BeFalse();
            fileInfos[0].VariableInfos.ToList()[0].IsLong.Should().BeFalse();
            fileInfos[0].VariableInfos.ToList()[0].IsFloat.Should().BeFalse();
            fileInfos[0].VariableInfos.ToList()[0].IsBool.Should().BeFalse();
        }

        if (expected == "bool")
        {
            fileInfos[0].VariableInfos.ToList()[0].IsBool.Should().BeTrue();

            fileInfos[0].VariableInfos.ToList()[0].IsString.Should().BeFalse();
            fileInfos[0].VariableInfos.ToList()[0].IsInt.Should().BeFalse();
            fileInfos[0].VariableInfos.ToList()[0].IsLong.Should().BeFalse();
            fileInfos[0].VariableInfos.ToList()[0].IsFloat.Should().BeFalse();
            fileInfos[0].VariableInfos.ToList()[0].IsDouble.Should().BeFalse();
        }
    }

    [Theory]
    [InlineData("integer", null)]
    [InlineData("integer", "")]
    [InlineData("integer", " ")]
    [InlineData("number", null)]
    [InlineData("number", "")]
    [InlineData("number", " ")]
    public void ProcessOpenApiTypesToCsharpTypes_Throw(
        string typeNameToken,
        string format)
    {
        // Arrange
        List<TypeInfo> fileInfos = new List<TypeInfo>
        {
            new TypeInfo
            {
                VariableInfos = new List<VariableInfo>
                {
                    new VariableInfo
                    {
                        OriginalVariableTypeNameToken = typeNameToken,
                        OriginalVariableTypeFormat = format
                    }
                }
            }
        };
        CSharpGenerator cSharpGenerator = new CSharpGenerator();

        // Act
        Action action = () =>
        {
            _sut.ProcessOpenApiTypesToCsharpTypes(fileInfos, cSharpGenerator.OpenApiCsharpTypeMap);
        };

        // Assert
        action.Should().Throw<GeneratorException>();
    }
}