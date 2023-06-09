namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.E2E.solution_base_namespace;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using FluentValidation;
using Generator;
using Xunit;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
[Trait("Category", "Generator")]
public class SolutionBaseNamespaceE2EShould : TestBase
{
    [Fact]
    public void ThrowWhenSolutionBaseNamespaceIsNotProvided()
    {
        // Arrange
        string currentPath = $"{BasePath}/E2E/solution_base_namespace/";
        string configFilePath = $"{currentPath}/base_namespace_missing.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowWhenSolutionBaseNamespaceIsEmpty()
    {
        // Arrange
        string currentPath = $"{BasePath}/E2E/solution_base_namespace/";
        string configFilePath = $"{currentPath}/base_namespace_missing.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowWhenSolutionBaseNamespaceIsSpaces()
    {
        // Arrange
        string currentPath = $"{BasePath}/E2E/solution_base_namespace/";
        string configFilePath = $"{currentPath}/base_namespace_spaces.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    public SolutionBaseNamespaceE2EShould(ITestOutputHelper outputHelper) : base(outputHelper)
    {
    }
}