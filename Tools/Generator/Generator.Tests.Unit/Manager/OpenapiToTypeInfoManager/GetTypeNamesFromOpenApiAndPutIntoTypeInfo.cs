namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager.OpenapiToTypeInfoManager;

using FluentAssertions;
using Generator;
using Generator.Models;
using Microsoft.OpenApi.Models;
using Xunit;

public partial class OpenApiToTypeInfoManager_Should
{
    [Fact]
    public void GetTypeNamesFromOpenApiAndPutIntoTypeInfo()
    {
        // Arrange
        (OpenApiDocument openApiFile, List<string> expected) testData = SchemasTestData.GetSchemaNamesTestData();
        List<TypeInfo> fileInfos = new List<TypeInfo>();

        // Act
        _sut.GetTypeNamesFromOpenApiAndAddToTypeInfo(fileInfos, testData.openApiFile);

        // Assert
        foreach (string expectedTypeName in testData.expected)
        {
            fileInfos.Where(p => p.OriginalTypeNameToken == expectedTypeName).ToList().Count.Should().Be(1);
        }
    }

    [Fact]
    public void GetTypeNamesFromOpenApiAndPutIntoTypeInfo_ThrowWhenNoTypeNamesInOpenApi()
    {
        // Arrange
        List<TypeInfo> fileInfos = new List<TypeInfo>();
        (OpenApiDocument openApiFile, List<string> expected) testData = SchemasTestData.GetSchemaNamesTestData();
        testData.openApiFile.Components.Schemas = new Dictionary<string, OpenApiSchema>();

        // Act
        Action action = () => { _sut.GetTypeNamesFromOpenApiAndAddToTypeInfo(fileInfos, testData.openApiFile); };

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GetTypeNamesFromOpenApiAndPutIntoTypeInfo_ThrowWhenFileInfoListIsNotEmpty()
    {
        // Arrange
        (OpenApiDocument openApiFile, List<string> expected) testData = SchemasTestData.GetSchemaNamesTestData();
        List<TypeInfo> fileInfos = new List<TypeInfo>
        {
            new TypeInfo()
        };

        // Act
        Action action = () => { _sut.GetTypeNamesFromOpenApiAndAddToTypeInfo(fileInfos, testData.openApiFile); };

        // Assert
        action.Should().Throw<GeneratorException>();
    }
}