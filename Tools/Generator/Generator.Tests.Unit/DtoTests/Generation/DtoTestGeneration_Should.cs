namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.DtoTests.Generation;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "Generator")]
public class DtoTestGeneration_Should : TestBase
{
    public DtoTestGeneration_Should(ITestOutputHelper outputHelper) : base(outputHelper)
    {
    }

    // [Fact]
    public void Generate()
    {
        // Arrange && Act
        Dictionary<string, string> filenames = new Dictionary<string, string>
        {
            { "PersonDto_Should.genref", "PersonDto_Should.cs" },
            { "DogDto_Should.genref", "DogDto_Should.cs" },
        };
        string currentPath = $"{BasePath}/DtoTests/Generation";
        string configFilePath = $"{currentPath}/config.json";
        Action action = () => { new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        foreach (KeyValuePair<string, string> filename in filenames)
        {
            List<string> reference = File.ReadLines($"{currentPath}/Reference/{filename.Key}").ToList();
            List<string> generated =
                File.ReadLines($"{currentPath}/dto_test_project_base_path/{filename.Value}").ToList();
            for (int i = 0; i < reference.Count(); i++)
            {
                if (ShouldBeIgnored(reference[i], "#TEST_IGNORE"))
                {
                    continue;
                }

                generated[i].Should().Be(reference[i],
                    $"There is a difference in line {i}. \n" +
                    $"Reference file: {BasePath}/Reference/{filename.Key} \n" +
                    $"Generated file: {BasePath}{filename.Value}");
            }
        }
    }
}