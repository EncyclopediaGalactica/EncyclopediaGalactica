namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.DtoGeneration.MultipleDtos;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class MultipleDtos_Generating_Should : TestBase
{
    [Fact]
    public async Task MultipleDtos()
    {
        // Arrange && Act
        Dictionary<int, List<string>> filenames = new Dictionary<int, List<string>>
        {
            { 1, new List<string> { "DogDtoReference.cs", "DogDto.cs" } },
            { 2, new List<string> { "CatDtoReference.cs", "CatDto.cs" } },
        };
        string configFilePath = $"{_basePath}/dto_filename_collection_should.json";
        CodeGenerator? codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate();

        // Assert
        foreach (KeyValuePair<int, List<string>> filename in filenames)
        {
            string referenceFilename = filename.Value.First(p => p.Contains("Reference"));
            string generatedFilename = filename.Value.First(p => !p.Contains("Reference"));

            List<string> referenceResult = File.ReadLines($"{_basePath}{referenceFilename}").ToList();
            List<string> generatedResult = File.ReadLines($"{_basePath}{generatedFilename}").ToList();
            for (int i = 0; i < referenceResult.Count(); i++)
            {
                generatedResult[i].Should().Be(referenceResult[i],
                    $"There is a difference in line {i}. \n" +
                    $"Reference file: {_basePath}{referenceFilename} \n" +
                    $"Generated file: {_basePath}{generatedFilename}");
            }
        }
    }
}