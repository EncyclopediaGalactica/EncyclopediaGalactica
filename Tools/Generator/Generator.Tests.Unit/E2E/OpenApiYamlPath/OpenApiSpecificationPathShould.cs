namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.E2E.OpenApiYamlPath;

using System.Diagnostics.CodeAnalysis;
using EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator;
using FluentAssertions;
using FluentValidation;
using Xunit;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
[Trait("Category", "Generator")]
public class OpenApiSpecificationPathShould : TestBase
{
    public OpenApiSpecificationPathShould(ITestOutputHelper outputHelper) : base(outputHelper)
    {
    }

    [Fact]
    public void ThrowWhenOpenApiSpecificationPathIsNotDefined()
    {
        // Arrange
        string currentPath = $"{BasePath}/E2E/openapi_yaml_path/";
        string configFilePath = $"{currentPath}/path_missing.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowWhenOpenApiSpecificationPathIsEmptyString()
    {
        // Arrange
        string currentPath = $"{BasePath}/E2E/openapi_yaml_path/";
        string configFilePath = $"{currentPath}/empty_string.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowWhenOpenApiSpecificationPathIsSpaces()
    {
        // Arrange
        string currentPath = $"{BasePath}/E2E/openapi_yaml_path/";
        string configFilePath = $"{currentPath}/spaces.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowWhenOpenApiSpecificationPathPointsToANotExistingDirectory()
    {
        // Arrange
        string currentPath = $"{BasePath}/E2E/openapi_yaml_path/";
        string configFilePath = $"{currentPath}/directory_does_not_exist.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(DirectoryNotFoundException));
    }

    [Fact]
    public void ThrowWhenOpenApiSpecificationPathPointsToANotExistingFile()
    {
        // Arrange
        string currentPath = $"{BasePath}/E2E/openapi_yaml_path/";
        string configFilePath = $"{currentPath}/file_does_not_exist.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(FileNotFoundException));
    }
}