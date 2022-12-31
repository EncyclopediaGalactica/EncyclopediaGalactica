namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.DtoInfoCollection;

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Generator;
using Xunit;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class PropertyTypeCollection_Should : TestBase
{
    [Fact]
    public void Collect_Types()
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
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "IntegerType").PropertyTypeName.Should().Be("int");
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "LongType").PropertyTypeName.Should().Be("long");
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "FloatType").PropertyTypeName.Should().Be("float");
        aSingleDto.PropertyInfos.First(p => p.PropertyName == "DoubleType").PropertyTypeName.Should().Be("double");
    }
}