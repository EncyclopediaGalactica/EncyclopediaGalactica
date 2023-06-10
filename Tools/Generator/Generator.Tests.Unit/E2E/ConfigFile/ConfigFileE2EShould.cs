namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.E2E.ConfigFile;

using System.Diagnostics.CodeAnalysis;
using EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
[Trait("Category", "Generator")]
public class ConfigFileE2EShould : TestBase
{
    [Fact]
    public void ThrowWhenConfigFilePathIsNotDefined()
    {
        // Arrange
        string currentPath = $"{BasePath}/E2E/config_file/";
        string configFilePath = $"{currentPath}/config.json";
        Action action = () => { new CodeGenerator.Builder().Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(GeneratorException));
    }

    [Fact]
    public void ThrowWhenConfigFileDoesNotExist()
    {
        // Arrange
        string currentPath = $"{BasePath}/E2E/config_file/";
        string configFilePath = $"{currentPath}/config.json";
        Action action = () => { new CodeGenerator.Builder().Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(GeneratorException));
    }

    public ConfigFileE2EShould(ITestOutputHelper outputHelper) : base(outputHelper)
    {
    }
}