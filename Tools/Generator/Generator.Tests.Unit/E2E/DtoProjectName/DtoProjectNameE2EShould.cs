namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.E2E.DtoProjectName;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using FluentValidation;
using Generator;
using Xunit;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
[Trait("Category", "Generator")]
public class DtoProjectNameE2EShould : TestBase
{
    private readonly string _currentPath;

    [Fact]
    public void ThrowWhenNoDtoProjectNameIsDefined()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/project_name_is_not_defined.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowWhenDtoProjectNameIsEmptyString()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/project_name_is_not_defined.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowWhenDtoProjectNameIsSpaces()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/project_name_is_spaces.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowWhenDtoProjectNameContainsOtherThanAlphanumericalAndDotCharacters()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/more_than_alphanum.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowWhenDtoProjectNameStartsWithOtherThanLetter()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/starts_with_not_letter.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowWhenDtoProjectNameContainsDotAndTheNextCharacterIsNotLetter()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/not_letter_after_dot_v1.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowWhenDtoProjectNameContainsDotAndTheNextCharacterIsNotLetterMultipleOccasion()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/not_letter_after_dot_v2.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    public void Generate(){}

    public void GenerateAndMakeTheNameTransformationWhenTheWholeDtoProjectNameIsLowerCase() {}

    public void GenerateAndMakeTheNameTransformationWhenTheWholeDtoProjectNameIsUpperCase() {}

    public void GenearteAndMakeTheNameTransofmrationWhenTheLetterAfterTheDotIsLowerCase() {}

    public DtoProjectNameE2EShould(ITestOutputHelper outputHelper) : base(outputHelper)
    {
        _currentPath = $"{BasePath}/E2E/DtoProjectName";
    }
}