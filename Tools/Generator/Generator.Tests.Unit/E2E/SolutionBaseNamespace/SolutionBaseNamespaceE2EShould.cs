namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.E2E.SolutionBaseNamespace;

using System.Diagnostics.CodeAnalysis;
using EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator;
using FluentAssertions;
using FluentValidation;
using Xunit;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
[Trait("Category", "Generator")]
public class SolutionBaseNamespaceE2EShould : TestBase
{
    private readonly string _currentPath;

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

    public SolutionBaseNamespaceE2EShould(ITestOutputHelper outputHelper) : base(outputHelper)
    {
        _currentPath = $"{BasePath}/E2E/SolutionBaseNamespace";
    }
}