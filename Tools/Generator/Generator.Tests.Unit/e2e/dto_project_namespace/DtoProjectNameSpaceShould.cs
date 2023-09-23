namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.e2e.dto_project_namespace;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using FluentValidation;
using Generator;
using Xunit;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
[Trait("Category", "Generator")]
[Trait("Category", "Generator-E2E")]
public class DtoProjectNameSpaceShould : TestBase
{
    private readonly string _currentPath;

    public DtoProjectNameSpaceShould(ITestOutputHelper outputHelper) : base(outputHelper)
    {
        _currentPath = $"{BasePath}/e2e/dto_project_namespace";
    }

    [Theory]
    [InlineData("empty.config.json")]
    [InlineData("not_defined.config.json")]
    [InlineData("includes_alphanumeric.config.json")]
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

    public async Task DtoProjectNamespace_IsTransformedWhenFirstLetterIsNotUppercase()
    {
    }
}