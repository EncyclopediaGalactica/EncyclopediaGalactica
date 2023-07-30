namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.e2e.ConfigFile;

using System.Diagnostics.CodeAnalysis;
using EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
[Trait("Category", "Generator")]
[Trait("Category", "Generator-E2E")]
public class ConfigFileE2EShould : TestBase
{
    private readonly string _currentPath;

    public ConfigFileE2EShould(ITestOutputHelper outputHelper) : base(outputHelper)
    {
        _currentPath = $"{BasePath}/E2E/ConfigFile";
    }

    [Fact]
    public void ThrowWhenConfigFilePathIsNotDefined()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/config.json";
        Action action = () => { new CodeGenerator.Builder().Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(GeneratorException));
    }

    [Fact]
    public void ThrowWhenConfigFileDoesNotExist()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/config.json";
        Action action = () => { new CodeGenerator.Builder().Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(GeneratorException));
    }
}