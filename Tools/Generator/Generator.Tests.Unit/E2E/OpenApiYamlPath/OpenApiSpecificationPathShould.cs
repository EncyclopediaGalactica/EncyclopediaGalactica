namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.E2E.OpenApiYamlPath;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using FluentValidation;
using Generator;
using Xunit;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
[Trait("Category", "Generator")]
[Trait("Category", "Generator-E2E")]
public class OpenApiSpecificationPathShould : TestBase
{
    private readonly string _currentPath;

    public OpenApiSpecificationPathShould(ITestOutputHelper outputHelper) : base(outputHelper)
    {
        _currentPath = $"{BasePath}/E2E/OpenApiYamlPath";
    }

    [Fact]
    public void ThrowWhenOpenApiSpecificationPathIsNotDefined()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/path_missing.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowWhenOpenApiSpecificationPathIsEmptyString()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/empty_string.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowWhenOpenApiSpecificationPathIsSpaces()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/spaces.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowWhenOpenApiSpecificationPathPointsToANotExistingDirectory()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/directory_does_not_exist.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(DirectoryNotFoundException));
    }

    [Fact]
    public void ThrowWhenOpenApiSpecificationPathPointsToANotExistingFile()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/file_does_not_exist.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(FileNotFoundException));
    }
}