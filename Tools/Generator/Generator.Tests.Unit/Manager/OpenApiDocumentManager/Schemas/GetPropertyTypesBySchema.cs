namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager.OpenApiDocumentManager.Schemas;

using FluentAssertions;
using Microsoft.OpenApi.Models;
using Xunit;

public partial class Schemas_Should
{
    [Theory]
    [InlineData("oneProperty")]
    [InlineData("onePropertyWithAllRequired")]
    [InlineData("twoProperties")]
    [InlineData("twoPropertiesWithAllRequired")]
    [InlineData("threeProperties")]
    [InlineData("threePropertiesWithAllRequired")]
    public void GetPropertyTypesBySchema(string schemaName)
    {
        // Arrange
        (OpenApiDocument openApi,
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> schemaWithPropertyNamesAndTypes)
            testData = SchemasTestData.GetPropertyTypesBySchemaTestData();

        // Act
        IDictionary<string, Dictionary<string, string>> result = Sut.Components
            .Schemas.GetPropertyTypesBySchema(schemaName, testData.openApi);

        // Assert
        result.Should().BeEquivalentTo(testData.schemaWithPropertyNamesAndTypes[schemaName]);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void GetPropertyTypesBySchema_Throws_WhenSchemaNameInputIsInvalid(string schemaName)
    {
        // Arrange
        (OpenApiDocument openApi,
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> schemaWithPropertyNamesAndTypes)
            testData = SchemasTestData.GetPropertyTypesBySchemaTestData();

        // Act
        Action action = () =>
        {
            Sut.Components
                .Schemas.GetPropertyTypesBySchema(schemaName, testData.openApi);
        };

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GetPropertyTypesBySchema_Throws_WhenNoSuchSchema()
    {
        // Arrange
        (OpenApiDocument openApi,
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> schemaWithPropertyNamesAndTypes)
            testData = SchemasTestData.GetPropertyTypesBySchemaTestData();
        string schemaName = "doesNotExist";

        // Act
        Action action = () => { Sut.Components.Schemas.GetPropertyTypesBySchema(schemaName, testData.openApi); };

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }
}