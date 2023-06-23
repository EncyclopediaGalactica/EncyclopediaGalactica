namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.E2E.DtoProjectNamespace;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using FluentValidation;
using Generator;
using Xunit;
using Xunit.Abstractions;

[Trait("Category", "Generator")]
[ExcludeFromCodeCoverage]
public class DtoProjectNamespaceE2EShould : TestBase
{
    private readonly string _currentPath;

    [Fact]
    public void Throw_WhenProvidedNamespaceValueIs_StringEmpty()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/string.empty.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void Throw_WhenProvidedNamespaceValueIs_Spaces()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/spaces.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    [Fact]
    public void NotThrow_WhenNoValueProvided()
    {
        // Arrange
        string configFilePath = $"{_currentPath}/no_namespace_provided.config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>().WithInnerException(typeof(ValidationException));
    }

    public void Transform_AndUseNameSpaceValue() {}

    public void UseDefaultValue_WhenNoValueProvided() {}

    public void UseProvidedValue() {}

    public DtoProjectNamespaceE2EShould(ITestOutputHelper outputHelper) : base(outputHelper)
    {
        _currentPath = $"{BasePath}/E2E/DtoProjectNamespace";
    }
}