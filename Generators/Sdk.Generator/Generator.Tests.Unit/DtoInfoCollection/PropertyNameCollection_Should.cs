namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.DtoInfoCollection;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class PropertyNameCollection_Should : TestBase
{
    [Fact]
    public void Collect_PropertyNames()
    {
        // Arrange && Act
        string configFilePath = $"{_basePath}/property_name_collection_should.json";
        CodeGenerator? codeGenerator = new CodeGenerator.Builder().SetPath(configFilePath).Generate();

        // Assert
        codeGenerator.Should().NotBeNull();
        codeGenerator.DtoFileInfos.Should().NotBeEmpty();
        codeGenerator.DtoFileInfos.Count.Should().Be(3);
        codeGenerator.DtoFileInfos.Where(p => p.FileName == "PetDto").ToList().Count.Should().Be(1);

        GeneratedFileInfo aSingleDto = codeGenerator.DtoFileInfos.First(p => p.FileName == "PetDto");
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "Id").Should().NotBeNull();
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "Name").Should().NotBeNull();
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "TagName").Should().NotBeNull();
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "IntegerTypeProperty").Should().NotBeNull();
    }
}