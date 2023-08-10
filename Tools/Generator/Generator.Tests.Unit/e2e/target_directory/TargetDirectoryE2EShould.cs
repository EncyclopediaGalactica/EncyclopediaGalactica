namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.e2e.TargetDirectory;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using FluentValidation;
using Generator;
using Xunit;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
[Trait("Category", "Generator")]
[Trait("Category", "Generator-E2E")]
public class TargetDirectoryE2EShould : TestBase
{
    private readonly string _currentPath;

    public TargetDirectoryE2EShould(ITestOutputHelper outputHelper) : base(outputHelper)
    {
        _currentPath = $"{BasePath}/e2e/target_directory";
    }

    [Fact]
    public void ThrowWhenTargetDirectoryIsNotDefined()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/path_missing.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowWhenTargetDirectoryIsEmpty()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/target_directory_is_empty.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowWhenTargetDirectoryIsSpaces()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/target_directory_is_empty.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowWhenTheProvidedRelativeTargetDirectoryPathDoesNotExist()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/target_directory_does_not_exist_relative.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(GeneratorException));
    }

    [Fact]
    public void ThrowWhenTheProvidedAbsoluteTargetDirectoryPathDoesNotExist()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/target_directory_does_not_exist_absolute.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(GeneratorException));
    }

    public void GenerateWhenTargetDirectoryIsProvidedAsRelativePath()
    {
    }

    public void GenerateWhenTargetDirectoryIsProvidedAsAbsolutePath()
    {
    }
}