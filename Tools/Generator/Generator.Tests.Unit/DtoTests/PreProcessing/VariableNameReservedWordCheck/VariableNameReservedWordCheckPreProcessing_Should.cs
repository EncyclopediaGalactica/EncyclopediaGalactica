namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.DtoTests.PreProcessing.
    VariableNameReservedWordCheck;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "Generator")]
public class VariableNameReservedWordCheckPreProcessing_Should : TestBase
{
    public VariableNameReservedWordCheckPreProcessing_Should(ITestOutputHelper outputHelper) : base(outputHelper)
    {
    }

    [Fact]
    public void Process_PropertyNames()
    {
        // Arrange && Act
        string currentPath = $"{BasePath}/DtoTests/PreProcessing/VariableNameReservedWordCheck";
        string configFilePath = $"{currentPath}/config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().Throw<GeneratorException>();
    }
}