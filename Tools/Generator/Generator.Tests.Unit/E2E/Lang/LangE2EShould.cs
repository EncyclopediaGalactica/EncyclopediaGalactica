namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.E2E.Lang;

using System.Diagnostics.CodeAnalysis;
using EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator;
using FluentAssertions;
using FluentValidation;
using Xunit;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
[Trait("Category","Generator")]
public class LangE2EShould : TestBase
{
    [Fact]
    public void ThrowExceptionWhenLangValueIsNotProvided()
    {
        // Arrange
        string currentPath = $"{BasePath}/E2E/lang/";
        string configFilePath = $"{currentPath}/missing_lang.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowExceptionWhenLangValueIsEmpty()
    {
        // Arrange
        string currentPath = $"{BasePath}/E2E/lang/";
        string configFilePath = $"{currentPath}/empty_lang.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void ThrowExceptionWhenLangValueIsSpace()
    {
        // Arrange
        string currentPath = $"{BasePath}/E2E/lang/";
        string configFilePath = $"{currentPath}/lang_is_space.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    public LangE2EShould(ITestOutputHelper outputHelper) : base(outputHelper)
    {
    }
}