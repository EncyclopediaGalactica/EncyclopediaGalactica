namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.E2E.lang;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using FluentValidation;
using Generator;
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
        string configFilePath = $"{currentPath}/config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    public LangE2EShould(ITestOutputHelper outputHelper) : base(outputHelper)
    {
    }
}