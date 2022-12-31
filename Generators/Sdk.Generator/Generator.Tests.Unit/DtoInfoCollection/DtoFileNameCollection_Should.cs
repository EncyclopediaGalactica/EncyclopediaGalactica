namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.DtoInfoCollection;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class DtoFileNameCollection_Should : TestBase
{
    [Fact]
    public void Collect_DtoNames()
    {
        // Arrange && Act
        string currentPath = $"{_basePath}/DtoInfoCollection";
        string configFilePath = $"{currentPath}/dto_filename_collection_should.json";
        CodeGenerator? codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate();

        // Assert
        codeGenerator.Should().NotBeNull();
        codeGenerator.DtoFileInfos.Should().NotBeEmpty();
        codeGenerator.DtoFileInfos.Count.Should().Be(3);
        codeGenerator.DtoFileInfos.Where(p => p.FileName == "PetDto").ToList().Count.Should().Be(1);
        codeGenerator.DtoFileInfos.Where(p => p.FileName == "NewPetDto").ToList().Count.Should().Be(1);
        codeGenerator.DtoFileInfos.Where(p => p.FileName == "ErrorModelDto").ToList().Count.Should().Be(1);
    }

    [Fact]
    public void Collect_Namespace()
    {
        // Arrange && Act
        string configFilePath = $"{_basePath}/dto_filename_collection_should.json";
        CodeGenerator? codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate();

        // Assert
        codeGenerator.Should().NotBeNull();
        codeGenerator.DtoFileInfos.Should().NotBeEmpty();
        codeGenerator.DtoFileInfos.Count.Should().Be(3);
        codeGenerator.DtoFileInfos.Where(p => p.FileName == "PetDto").ToList().Count.Should().Be(1);
        codeGenerator.DtoFileInfos.First(p => p.FileName == "PetDto")
            .Namespace.Should().Be("EncyclopediaGalactica.Generators");
    }

    [Fact]
    public void Collect_Namespace_Typo()
    {
        // Arrange && Act
        string configFilePath = $"{_basePath}/dto_filename_collection_should.json";
        CodeGenerator? codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate();

        // Assert
        codeGenerator.Should().NotBeNull();
        codeGenerator.DtoFileInfos.Should().NotBeEmpty();
        codeGenerator.DtoFileInfos.Count.Should().Be(3);
        codeGenerator.DtoFileInfos.Where(p => p.FileName == "PetDto").ToList().Count.Should().Be(1);
        codeGenerator.DtoFileInfos.First(p => p.FileName == "PetDto")
            .Namespace.Should().Be("EncyclopediaGalactica.Generators");
    }
}