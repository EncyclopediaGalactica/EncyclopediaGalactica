namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager.OpenApiDocumentManager.Schemas;

using FluentAssertions;
using Microsoft.OpenApi.Models;
using TestBase;
using Xunit;

public partial class Schemas_Should : TestBase
{
    [Theory]
    [InlineData("oneProperty")]
    [InlineData("onePropertyWithAllRequired")]
    [InlineData("twoProperties")]
    [InlineData("twoPropertiesWithAllRequired")]
    [InlineData("threeProperties")]
    [InlineData("threePropertiesWithAllRequired")]
    public void GetPropertyNamesBySchema(string schemaName)
    {
        // Arrange
        (OpenApiDocument openApi, Dictionary<string, List<string>> schemaNames) testData =
            SchemasTestData.GetPropertyNamesBySchemaTestData();

        // Act
        List<string> result = Sut.Components.Schemas.GetPropertyNamesBySchema(schemaName, testData.openApi);

        //
        result.Should().BeEquivalentTo(testData.schemaNames[schemaName]);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void GetPropertyNamesBySchema_Throw_ArgumentException_WhenSchemaNameIsInvalid(string input)
    {
        // Arrange
        (OpenApiDocument openApi, Dictionary<string, List<string>> schemaNames) testData =
            SchemasTestData.GetPropertyNamesBySchemaTestData();

        // Act
        Action action = () => { Sut.Components.Schemas.GetPropertyNamesBySchema(input, testData.openApi); };

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GetPropertyNamesBySchema_ReturnEmptyList_WhenSchemasIsEmpty()
    {
        // Arrange
        (OpenApiDocument openApi, Dictionary<string, List<string>> schemaNames) testData =
            SchemasTestData.GetPropertyNamesBySchemaTestData();
        testData.openApi.Components.Schemas = new Dictionary<string, OpenApiSchema>();

        // Act
        List<string> result = Sut.Components.Schemas.GetPropertyNamesBySchema("input", testData.openApi);

        // Assert
        result.Count.Should().Be(0);
    }
}