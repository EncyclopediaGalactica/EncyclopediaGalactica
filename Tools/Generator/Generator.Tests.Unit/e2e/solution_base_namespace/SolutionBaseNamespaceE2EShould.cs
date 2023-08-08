namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.e2e.SolutionBaseNamespace;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using FluentValidation;
using Generator;
using Xunit;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
[Trait("Category", "Generator")]
[Trait("Category", "Generator-E2E")]
public class SolutionBaseNamespaceE2EShould : TestBase
{
    private readonly string _currentPath;

    public SolutionBaseNamespaceE2EShould(ITestOutputHelper outputHelper) : base(outputHelper)
    {
        _currentPath = $"{BasePath}/E2E/solution_base_namespace";
    }

    [Fact]
    public void ThrowWhenSolutionBaseNamespaceIsNotProvided()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/base_namespace_missing.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowWhenSolutionBaseNamespaceIsEmpty()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/base_namespace_missing.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowWhenSolutionBaseNamespaceIsSpaces()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/base_namespace_spaces.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowWhenSolutionBaseNamespaceStartsWithOtherThanLetter()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/base_namespace_starts_with_not_a_letter.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    public void GenerateAndTransformSolutionBaseNamespace()
    {
        // see specification
    }
}