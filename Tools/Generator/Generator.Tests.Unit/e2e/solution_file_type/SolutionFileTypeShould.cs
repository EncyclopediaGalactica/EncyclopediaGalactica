namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.e2e.solution_file_type;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using FluentValidation;
using Generator;
using Xunit;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
[Trait("Category", "Generator")]
[Trait("Category", "Generator-E2E")]
public class SolutionFileTypeShould : TestBase
{
    private readonly string _currentPath;

    public SolutionFileTypeShould(ITestOutputHelper outputHelper) : base(outputHelper)
    {
        _currentPath = $"{BasePath}/e2e/solution_file_type";
    }

    [Theory]
    [InlineData("empty.config.json")]
    [InlineData("not_defined.config.json")]
    [InlineData("not_only_letters.config.json")]
    [InlineData("whitespace.config.json")]
    public void SolutionFileType_Throws_WhenInputIsInvalid(string fileName)
    {
        // Arrange
        string configFilePath = $"{_currentPath}/{fileName}";

        // Act
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }
}