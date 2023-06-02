namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager.OpenApiDocumentManager.Schemas;

using FluentAssertions;
using Microsoft.OpenApi.Models;
using Xunit;

public partial class Schemas_Should
{
    [Fact]
    public void GetSchemaNames()
    {
        // Arrange && Act
        (OpenApiDocument openApi, List<string> expected) testData = SchemasTestData.GetSchemaNamesTestData();
        List<string> result = Sut.Components.Schemas.GetSchemaNames(testData.openApi);

        // Assert
        result.Should().BeEquivalentTo(testData.expected);
    }

    [Fact]
    public void GetSchemaNames_Throw_WhenSchemaDoesNotHaveTypes()
    {
        // Arrange
        (OpenApiDocument openApi, List<string> expected) testData = SchemasTestData.GetSchemaNamesTestData();
        testData.openApi.Components.Schemas = new Dictionary<string, OpenApiSchema>();

        // Act
        Action action = () => { Sut.Components.Schemas.GetSchemaNames(testData.openApi); };

        // Assert
        action.Should().Throw<Exception>();
    }
}