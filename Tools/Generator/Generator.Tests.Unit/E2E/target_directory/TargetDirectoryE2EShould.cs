namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.E2E.target_directory;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using FluentValidation;
using Generator;
using Xunit;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
[Trait("Category", "Generator")]
public class TargetDirectoryE2EShould : TestBase
{
    [Fact]
    public void ThrowWhenTargetDirectoryIsNotDefined()
    {
        // Arrange
        string currentPath = $"{BasePath}/E2E/target_directory/";
        string configFilePath = $"{currentPath}/path_missing.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowWhenTargetDirectoryIsEmpty()
    {
        // Arrange
        string currentPath = $"{BasePath}/E2E/target_directory/";
        string configFilePath = $"{currentPath}/target_directory_is_empty.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowWhenTargetDirectoryIsSpaces()
    {
        // Arrange
        string currentPath = $"{BasePath}/E2E/target_directory/";
        string configFilePath = $"{currentPath}/target_directory_is_empty.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    public TargetDirectoryE2EShould(ITestOutputHelper outputHelper) : base(outputHelper)
    {
    }
}