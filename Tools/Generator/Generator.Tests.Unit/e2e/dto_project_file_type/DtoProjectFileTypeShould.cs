namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.e2e.dto_project_file_type;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using FluentValidation;
using Generator;
using Xunit;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
[Trait("Category", "Generator")]
[Trait("Category", "Generator-E2E")]
public class DtoProjectFileTypeShould : TestBase
{
    private readonly string _currentPath;

    public DtoProjectFileTypeShould(ITestOutputHelper outputHelper) : base(outputHelper)
    {
        _currentPath = $"{BasePath}/e2e/dto_project_file_type";
    }

    [Theory]
    [InlineData("empty.config.json")]
    [InlineData("not_defined.config.json")]
    [InlineData("not_only_alphanumeric.config.json")]
    public void DtoProjectFiletype_Throws_WhenConfigurationIsInvalid(string fileName)
    {
        // Arrange
        string configFilePath = $"{_currentPath}/{fileName}";

        // Act
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    public void DtoProjectFileType_TransformsValueToLowerCase()
    {
    }
}