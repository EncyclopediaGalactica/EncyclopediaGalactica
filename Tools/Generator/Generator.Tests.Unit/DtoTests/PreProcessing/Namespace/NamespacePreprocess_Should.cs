namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.DtoTests.PreProcessing.Namespace;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "Generator")]
public class NamespacePreprocess_Should : TestBase
{
    public NamespacePreprocess_Should(ITestOutputHelper outputHelper) : base(outputHelper)
    {
    }

    // [Fact]
    public void PreProcess_DtoTestNamespaceIsNotProvided_SingleFile()
    {
        // Arrange && Act
        string currentPath = $"{BasePath}/DtoTests/PreProcessing/Namespace";
        string configFilePath = $"{currentPath}/dto_test_namespace_is_not_provided_single.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator!.SpecificCodeGenerator.DtoTestTypeInfos.Should().NotBeEmpty();
        codeGenerator.SpecificCodeGenerator.DtoTestTypeInfos.First(p => p.FileName == "SingleFilenameDto_Should.cs")
            .Namespace.Should().Be("Solution.Nmspace.Single");
    }

    // [Fact]
    public void PreProcess_DtoTestNamespaceIsNotProvided_MultipleFile()
    {
        // Arrange && Act
        string currentPath = $"{BasePath}/DtoTests/PreProcessing/Namespace";
        string configFilePath = $"{currentPath}/dto_test_namespace_is_not_provided_multiple.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator!.SpecificCodeGenerator.DtoTestTypeInfos.Should().NotBeEmpty();
        codeGenerator.SpecificCodeGenerator.DtoTestTypeInfos.First(p => p.FileName == "FirstDto_Should.cs")
            .Namespace.Should().Be("Solution.Nmspace.Multiple");
        codeGenerator.SpecificCodeGenerator.DtoTestTypeInfos.First(p => p.FileName == "SecondDto_Should.cs")
            .Namespace.Should().Be("Solution.Nmspace.Multiple");
    }

    // [Fact]
    public void PreProcess_DtoTestNamespaceIsProvided_SingleFile()
    {
        // Arrange && Act
        string currentPath = $"{BasePath}/DtoTests/PreProcessing/Namespace";
        string configFilePath = $"{currentPath}/dto_test_namespace_is_provided_single.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator!.SpecificCodeGenerator.DtoTestTypeInfos.Should().NotBeEmpty();
        codeGenerator.SpecificCodeGenerator.DtoTestTypeInfos
            .First(p => p.FileName == "SingleDto_Should.cs")
            .Namespace.Should().Be("Solution.Nmspace.Prov.Single.Dto.Tests");
    }

    // [Fact]
    public void PreProcess_DtoTestNamespaceIsProvided_MultipleFile()
    {
        // Arrange && Act
        string currentPath = $"{BasePath}/DtoTests/PreProcessing/Namespace";
        string configFilePath = $"{currentPath}/dto_test_namespace_is_provided_multiple.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator!.SpecificCodeGenerator.DtoTestTypeInfos.Should().NotBeEmpty();
        codeGenerator.SpecificCodeGenerator.DtoTestTypeInfos
            .First(p => p.FileName == "FirstNamespaceProvidedDto_Should.cs")
            .Namespace.Should().Be("Sol.Nmspace.Dto.Tests");
        codeGenerator.SpecificCodeGenerator.DtoTestTypeInfos
            .First(p => p.FileName == "SecondNamespaceProvidedDto_Should.cs")
            .Namespace.Should().Be("Sol.Nmspace.Dto.Tests");
    }
}