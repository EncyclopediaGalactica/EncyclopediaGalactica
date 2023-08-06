namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager.OpenApiDocumentManager.Schemas;

using FluentAssertions;
using Microsoft.OpenApi.Models;
using Xunit;

public partial class Schemas_Should
{
    [Theory]
    [InlineData("twoRequired")]
    [InlineData("oneRequired")]
    [InlineData("noneRequired")]
    public void GetRequiredPropertiesBySchema(string schemaName)
    {
        // Arrange
        (OpenApiDocument openApi, Dictionary<string, List<string>> expected) testData =
            SchemasTestData.GetRequiredPropertiesBySchemaTestData();

        // Act
        List<string> result = Sut.Components.Schemas.GetRequiredPropertiesBySchema(schemaName, testData.openApi);

        // Assert
        result.Should().BeEquivalentTo(testData.expected[schemaName]);
    }
}