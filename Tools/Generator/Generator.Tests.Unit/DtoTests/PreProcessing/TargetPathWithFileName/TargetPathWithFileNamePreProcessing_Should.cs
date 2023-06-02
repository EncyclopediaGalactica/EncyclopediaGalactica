namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.DtoTests.PreProcessing.TargetPathWithFileName;

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
        string currentPath = $"{BasePath}/DtoTests/PreProcessing/TargetPathWithFileName";
        string configFilePath = $"{currentPath}/basepath_config.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator!.SpecificCodeGenerator.DtoTestTypeInfos.Should().NotBeEmpty();
        codeGenerator.SpecificCodeGenerator.DtoTestTypeInfos.Count.Should().Be(1);
        codeGenerator.SpecificCodeGenerator.DtoTestTypeInfos
            .Find(p => p.TypeName == "TargetPathWithFileNameDto_Should")
            .Should().NotBeNull();

        TypeInfo? typeInfo = codeGenerator.SpecificCodeGenerator.DtoTestTypeInfos
            .Find(p => p.TypeName == "TargetPathWithFileNameDto_Should");
        typeInfo.Should().NotBeNull();
        typeInfo!.TargetPathWithFileName.Should()
            .Be(
                $"{BasePath}/DtoTests/PreProcessing/TargetPathWithFileName/dto_test_project_base_path/TargetPathWithFileNameDto_Should.cs");
    }

    [Fact]
    public void PreProcess_WhenBasePath_AndDtoTestProject()
    {
        // Arrange && Act
        string currentPath = $"{BasePath}/DtoTests/PreProcessing/TargetPathWithFileName";
        string configFilePath = $"{currentPath}/basepath_and_dtotestproject_config.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator!.SpecificCodeGenerator.DtoTestTypeInfos.Should().NotBeEmpty();
        codeGenerator.SpecificCodeGenerator.DtoTestTypeInfos.Count.Should().Be(1);
        codeGenerator.SpecificCodeGenerator.DtoTestTypeInfos
            .Find(p => p.TypeName == "TargetPathWithFileNameDto_Should")
            .Should().NotBeNull();

        TypeInfo? typeInfo = codeGenerator.SpecificCodeGenerator.DtoTestTypeInfos
            .Find(p => p.TypeName == "TargetPathWithFileNameDto_Should");
        typeInfo.Should().NotBeNull();
        typeInfo!.TargetPathWithFileName.Should()
            .Be($"{BasePath}/DtoTests/PreProcessing/TargetPathWithFileName/testprojectBasepath/" +
                $"TargetPathWithFileNameDto_Should.cs");
    }

    [Fact]
    public void PreProcess_WhenBasePath_AndDtoTestProject_AndAdditionalPath()
    {
        // Arrange && Act
        string currentPath = $"{BasePath}/DtoTests/PreProcessing/TargetPathWithFileName";
        string configFilePath = $"{currentPath}/basepath_and_dtotestproject_additionalpath_config.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator!.SpecificCodeGenerator.DtoTestTypeInfos.Should().NotBeEmpty();
        codeGenerator.SpecificCodeGenerator.DtoTestTypeInfos.Count.Should().Be(1);
        codeGenerator.SpecificCodeGenerator.DtoTestTypeInfos
            .Find(p => p.TypeName == "TargetPathWithFileNameDto_Should")
            .Should().NotBeNull();

        TypeInfo? typeInfo = codeGenerator.SpecificCodeGenerator.DtoTestTypeInfos
            .Find(p => p.TypeName == "TargetPathWithFileNameDto_Should");
        typeInfo.Should().NotBeNull();
        typeInfo!.TargetPathWithFileName.Should()
            .Be($"{BasePath}/DtoTests/PreProcessing/TargetPathWithFileName/testprojectBasepath/additionalPath/" +
                $"TargetPathWithFileNameDto_Should.cs");
    }
}