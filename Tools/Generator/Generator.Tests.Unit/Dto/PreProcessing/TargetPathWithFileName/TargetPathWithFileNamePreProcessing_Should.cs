namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.PreProcessing.TargetPathWithFileName;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Generator.Models;
using Xunit;
using Xunit.Abstractions;

[Collection("PreProcessing")]
[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "Generator")]
public class TargetPathWithFileNamePreProcessing_Should : TestBase
{
    public TargetPathWithFileNamePreProcessing_Should(ITestOutputHelper outputHelper) : base(outputHelper)
    {
    }

    [Fact]
    public void PreProcess_WhenOnlyBasePath()
    {
        // Arrange && Act
        string currentPath = $"{BasePath}/Dto/PreProcessing/TargetPathWithFileName";
        string configFilePath = $"{currentPath}/basepath_config.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator!.SpecificCodeGenerator.DtoTypeInfos.Should().NotBeEmpty();
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Count.Should().Be(1);
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos
            .Find(p => p.TypeName == "TargetPathWithFileNameDto")
            .Should().NotBeNull();

        TypeInfo? typeInfo = codeGenerator.SpecificCodeGenerator.DtoTypeInfos
            .Find(p => p.TypeName == "TargetPathWithFileNameDto");
        typeInfo.Should().NotBeNull();
        typeInfo!.TargetPathWithFileName.Should()
            .Be(
                $"{BasePath}/Dto/PreProcessing/TargetPathWithFileName/dto_project_base_path/TargetPathWithFileNameDto.cs");
    }

    [Fact]
    public void PreProcess_WhenBasePath_AndDtoTestProject()
    {
        // Arrange && Act
        string currentPath = $"{BasePath}/Dto/PreProcessing/TargetPathWithFileName";
        string configFilePath = $"{currentPath}/basepath_and_dtotestproject_config.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator!.SpecificCodeGenerator.DtoTypeInfos.Should().NotBeEmpty();
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Count.Should().Be(1);
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos
            .Find(p => p.TypeName == "TargetPathWithFileNameDto")
            .Should().NotBeNull();

        TypeInfo? typeInfo = codeGenerator.SpecificCodeGenerator.DtoTypeInfos
            .Find(p => p.TypeName == "TargetPathWithFileNameDto");
        typeInfo.Should().NotBeNull();
        typeInfo!.TargetPathWithFileName.Should()
            .Be($"{BasePath}/Dto/PreProcessing/TargetPathWithFileName/projectBasepath/" +
                $"TargetPathWithFileNameDto.cs");
    }

    [Fact]
    public void PreProcess_WhenBasePath_AndDtoTestProject_AndAdditionalPath()
    {
        // Arrange && Act
        string currentPath = $"{BasePath}/Dto/PreProcessing/TargetPathWithFileName";
        string configFilePath = $"{currentPath}/basepath_and_dtotestproject_additionalpath_config.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator!.SpecificCodeGenerator.DtoTypeInfos.Should().NotBeEmpty();
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Count.Should().Be(1);
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos
            .Find(p => p.TypeName == "TargetPathWithFileNameDto")
            .Should().NotBeNull();

        TypeInfo? typeInfo = codeGenerator.SpecificCodeGenerator.DtoTypeInfos
            .Find(p => p.TypeName == "TargetPathWithFileNameDto");
        typeInfo.Should().NotBeNull();
        typeInfo!.TargetPathWithFileName.Should()
            .Be($"{BasePath}/Dto/PreProcessing/TargetPathWithFileName/projectBasepath/additionalPath/" +
                $"TargetPathWithFileNameDto.cs");
    }
}