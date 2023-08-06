namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Dto.PreProcessing.Filename;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "Generator")]
public class FileNamePreProcessing_Should : TestBase
{
    public FileNamePreProcessing_Should(ITestOutputHelper outputHelper) : base(outputHelper)
    {
    }

    // [Fact]
    public void PreProcess_FileName()
    {
        // Arrange && Act
        string currentPath = $"{BasePath}/Dto/PreProcessing/Filename";
        string configFilePath = $"{currentPath}/config.json";
        CodeGenerator? codeGenerator = null;
        Action action = () => { codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate(); };

        // Assert
        action.Should().NotThrow();
        codeGenerator.Should().NotBeNull();
        codeGenerator!.SpecificCodeGenerator.Should().NotBeNull();
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos.Should().NotBeEmpty();
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos
            .Where(p => p.FileName == "PetDto.cs").ToList().Count.Should().Be(1);
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos
            .Where(p => p.FileName == "NewPetDto.cs").ToList().Count.Should().Be(1);
        codeGenerator.SpecificCodeGenerator.DtoTypeInfos
            .Where(p => p.FileName == "ErrorModelDto.cs").ToList().Count.Should().Be(1);
    }
}