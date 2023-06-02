namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.PreProcessing.NullableVariableType;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Generator.Models;
using Xunit;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "Generator")]
public class NullableVariableTypePreProcessing_Should : TestBase
{
    public NullableVariableTypePreProcessing_Should(ITestOutputHelper outputHelper) : base(outputHelper)
    {
    }

    [Fact]
    public void PreProcess_NullablePropertyTypes()
    {
        // Arrange && Act
        string currentPath = $"{BasePath}/Dto/PreProcessing/NullableVariableType";
        string configFilePath = $"{currentPath}/config.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator!.SpecificCodeGenerator.Should().NotBeNull();
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Should().NotBeEmpty();
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Count.Should().Be(1);
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Where(p => p.FileName == "PetDto.cs").ToList().Count.Should()
            .Be(1);

        TypeInfo aSingleDto = codeGenerator.SpecificCodeGenerator.DtoTypeInfos.First(p => p.FileName == "PetDto.cs");

        aSingleDto.VariableInfos.First(p => p.VariableName == "StringType").VariableTypeName.Should().Be("string");
        aSingleDto.VariableInfos.First(p => p.VariableName == "StringType").IsNullable.Should().BeFalse();

        aSingleDto.VariableInfos.First(p => p.VariableName == "StringTypeNullable")
            .VariableTypeName.Should().Be("string");
        aSingleDto.VariableInfos.First(p => p.VariableName == "StringTypeNullable").IsNullable.Should().BeTrue();

        aSingleDto.VariableInfos.First(p => p.VariableName == "IntegerType").VariableTypeName.Should().Be("int");
        aSingleDto.VariableInfos.First(p => p.VariableName == "IntegerType").IsNullable.Should().BeFalse();

        aSingleDto.VariableInfos.First(p => p.VariableName == "IntegerTypeNullable")
            .VariableTypeName.Should().Be("int");
        aSingleDto.VariableInfos.First(p => p.VariableName == "IntegerTypeNullable").IsNullable.Should().BeTrue();

        aSingleDto.VariableInfos.First(p => p.VariableName == "LongType").VariableTypeName.Should().Be("long");
        aSingleDto.VariableInfos.First(p => p.VariableName == "LongType").IsNullable.Should().BeFalse();

        aSingleDto.VariableInfos.First(p => p.VariableName == "LongTypeNullable")
            .VariableTypeName.Should().Be("long");
        aSingleDto.VariableInfos.First(p => p.VariableName == "LongTypeNullable").IsNullable.Should().BeTrue();

        aSingleDto.VariableInfos.First(p => p.VariableName == "FloatType")
            .VariableTypeName.Should().Be("float");
        aSingleDto.VariableInfos.First(p => p.VariableName == "FloatType").IsNullable.Should().BeFalse();

        aSingleDto.VariableInfos.First(p => p.VariableName == "FloatTypeNullable")
            .VariableTypeName.Should().Be("float");
        aSingleDto.VariableInfos.First(p => p.VariableName == "FloatTypeNullable").IsNullable.Should().BeTrue();

        aSingleDto.VariableInfos.First(p => p.VariableName == "DoubleType")
            .VariableTypeName.Should().Be("double");
        aSingleDto.VariableInfos.First(p => p.VariableName == "DoubleType").IsNullable.Should().BeFalse();

        aSingleDto.VariableInfos.First(p => p.VariableName == "DoubleTypeNullable")
            .VariableTypeName.Should().Be("double");
        aSingleDto.VariableInfos.First(p => p.VariableName == "DoubleTypeNullable").IsNullable.Should().BeTrue();
    }
}