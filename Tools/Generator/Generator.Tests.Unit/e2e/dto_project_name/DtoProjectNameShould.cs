namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.e2e.dto_project_name;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using FluentValidation;
using Generator;
using Xunit;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
[Trait("Category", "Generator")]
[Trait("Category", "Generator-E2E")]
public class DtoProjectNameShould : TestBase
{
    private readonly string _currentPath;

    public DtoProjectNameShould(ITestOutputHelper outputHelper) : base(outputHelper)
    {
        _currentPath = $"{BasePath}/e2e/dto_project_name";
    }

    [Theory]
    [InlineData("empty.config.json")]
    [InlineData("not_defined.config.json")]
    [InlineData("number_right_after_the_dot.config.json")]
    [InlineData("prohibited_special_char.config.json")]
    [InlineData("prohibited_special_char_after_dot.config.json")]
    [InlineData("starts_with_number.config.json")]
    [InlineData("whitespace.config.json")]
    public void DtoProjectName_Throws_WhenInputIsInvalid(string fileName)
    {
        // Arrange
        string configFilePath = $"{_currentPath}/{fileName}";

        // Act
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }
}