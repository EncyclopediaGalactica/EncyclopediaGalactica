namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.DtoGeneration.SingleDtoTests;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class SingleDto_Generating_Should : TestBase
{
    [Fact]
    public async Task SingleDtoTest()
    {
        // Arrange && Act
        string referenceFileName = "PetDtoReference.cs";
        string generatedFileName = "PetDto.cs";
        string configFilePath = $"{_basePath}/dto_filename_collection_should.json";
        CodeGenerator? codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate();

        // Assert
        List<string> referenceResult = File.ReadLines($"{_basePath}{referenceFileName}").ToList();
        List<string> generatedResult = File.ReadLines($"{_basePath}{generatedFileName}").ToList();
        for (int i = 0; i < referenceResult.Count(); i++)
        {
            generatedResult[i].Should().Be(referenceResult[i],
                $"There is a difference in line {i}. \n" +
                $"Reference file: {_basePath}{referenceFileName} \n" +
                $"Generated file: {_basePath}{generatedFileName}");
        }
    }
}