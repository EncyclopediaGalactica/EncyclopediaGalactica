namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.DtoTests.PreProcessing.
    NamespaceReservedWordsCheck;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "Generator")]
public class ReservedWordsCheck_Should : TestBase
{
    public ReservedWordsCheck_Should(ITestOutputHelper outputHelper) : base(outputHelper)
    {
    }

    [Fact]
    public void Throw_WhenBaseNamespaceIsAReservedWord()
    {
        // Arrange && Act
        string currentPath = $"{BasePath}/DtoTests/PreProcessing/NamespaceReservedWordsCheck";
        string configFilePath = $"{currentPath}/base_namespace_is_reserved_word.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>();
    }

    [Fact]
    public void Throw_WhenBaseNamespaceIncludesAReservedWord_AtStart()
    {
        // Arrange && Act
        string currentPath = $"{BasePath}/DtoTests/PreProcessing/NamespaceReservedWordsCheck";
        string configFilePath = $"{currentPath}/base_namespace_includes_a_reserved_word1.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>();
    }

    [Fact]
    public void Throw_WhenBaseNamespaceIncludesAReservedWord_InTheMiddle()
    {
        // Arrange && Act
        string currentPath = $"{BasePath}/DtoTests/PreProcessing/NamespaceReservedWordsCheck";
        string configFilePath = $"{currentPath}/base_namespace_includes_a_reserved_word2.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>();
    }

    [Fact]
    public void Throw_WhenBaseNamespaceIncludesAReservedWord_AtTheEnd()
    {
        // Arrange && Act
        string currentPath = $"{BasePath}/DtoTests/PreProcessing/NamespaceReservedWordsCheck";
        string configFilePath = $"{currentPath}/base_namespace_includes_a_reserved_word3.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>();
    }
}