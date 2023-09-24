namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.e2e.dto_project_path;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using FluentValidation;
using Generator;
using Xunit;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
[Trait("Category", "Generator")]
[Trait("Category", "Generator-E2E")]
public class DtoProjectPathShould : TestBase
{
    private readonly string _currentPath;

    public DtoProjectPathShould(ITestOutputHelper outputHelper) : base(outputHelper)
    {
        _currentPath = $"{BasePath}/e2e/dto_project_path";
    }

    [Theory]
    [InlineData("empty.config.json")]
    [InlineData("going_back_path.config.json")]
    [InlineData("non_alphanumeric_character.config.json")]
    [InlineData("not_defined.config.json")]
    [InlineData("whitespace.config.json")]
    [InlineData("absolute_path.config.json")]
    public void DtoProjectPath_Throws_WhenInputIsInvalid(string fileName)
    {
        // Arrange
        string configFilePath = $"{_currentPath}/{fileName}";

        // Act
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    public async Task DtoProjectPath_IsTransformedWhenFirstLetterIsNotUppercase()
    {
    }
}