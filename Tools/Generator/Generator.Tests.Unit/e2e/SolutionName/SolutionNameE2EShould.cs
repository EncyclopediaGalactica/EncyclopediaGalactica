namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.e2e.SolutionName;

using System.Diagnostics.CodeAnalysis;
using EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator;
using FluentAssertions;
using FluentValidation;
using Xunit;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
[Trait("Category", "Generator")]
[Trait("Category", "Generator-E2E")]
public class SolutionNameE2EShould : TestBase
{
    private readonly string _currentPath;

    public SolutionNameE2EShould(ITestOutputHelper outputHelper) : base(outputHelper)
    {
        _currentPath = $"{BasePath}/E2E/SolutionName";
    }

    [Fact]
    public void ThrowWhenSolutionNameValueDoesNotExist()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/solution_name_is_missing.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowWhenSolutionNameIsEmpty()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/solution_name_empty.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowWhenSolutionNameIsSpace()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/solution_name_is_spaces.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowWhenSolutionNameContainsOtherSpecialCharactersThanDot()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/special_characters.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowWhenSolutionNameStartsWithNumber()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/starts_with_number.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    public void GenerateAndFixSolutionNameWhenItHasADotAndTheCharAfterTheDotIsNotUpperCase()
    {
    }

    public void GenerateWhenSolutionNameIsLowerCase()
    {
    }

    public void GenerateWhenSolutionNameIsUpperCase()
    {
    }
}