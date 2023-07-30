namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.e2e.lang;

using System.Diagnostics.CodeAnalysis;
using EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator;
using FluentAssertions;
using FluentValidation;
using Xunit;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
[Trait("Category", "Generator")]
[Trait("Category", "Generator-E2E")]
public class LangE2EShould : TestBase
{
    private readonly string _currentPath;

    public LangE2EShould(ITestOutputHelper outputHelper) : base(outputHelper)
    {
        _currentPath = $"{BasePath}/e2e/lang";
    }

    [Fact]
    public void ThrowExceptionWhenLangValueIsNotProvided()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/missing_lang.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should()
            .Throw<GeneratorException>()
            .WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowExceptionWhenLangValueIsEmpty()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/empty_lang.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should()
            .Throw<GeneratorException>()
            .WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowExceptionWhenLangValueIsSpace()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/lang_is_space.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should()
            .Throw<GeneratorException>()
            .WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowExceptionWhenLangValueIsSomethingNotAccepted()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/lang_is_space.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should()
            .Throw<GeneratorException>()
            .WithInnerException(typeof(ValidationException));
    }

    public void GenerateE2E()
    {
        // Arrange
        Dictionary<string, string> files = new Dictionary<string, string>
        {
            {
                $"{_currentPath}/e2e/TestSolutionTemplate/TestSolution.Dto/PersonDto.cs",
                $"{_currentPath}/reference/TestSolutionTemplate/TestSolution.Dto/PersonDto.cs"
            },
            {
                $"{_currentPath}/e2e/TestSolutionTemplate/TestSolution.Dto/Address.cs",
                $"{_currentPath}/reference/TestSolutionTemplate/TestSolution.Dto/AddressDto.cs"
            },
        };
        string configFilePath = $"{_currentPath}/e2e.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        foreach (KeyValuePair<string, string> file in files)
        {
            // CompareFileLineByLine(file.Key, file.Value);
        }
    }
}