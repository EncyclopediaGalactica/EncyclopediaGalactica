namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager.OpenApiDocumentManager.Schemas;

using FluentAssertions;
using Microsoft.OpenApi.Models;
using Xunit;

public partial class Schemas_Should
{
    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    public void GetSchemas(int amountOfSchemas)
    {
        // Arrange
        (OpenApiDocument OpenApi, List<string> expectedSchemaNames)? data = null;

        if (amountOfSchemas == 1)
        {
            data = SchemasTestData.Get1SchemaTestData();
        }

        if (amountOfSchemas == 3)
        {
            data = SchemasTestData.Get3SchemasTestData();
        }

        if (data is null) throw new Exception($"{nameof(data)} is null");

        // Act
        IDictionary<string, OpenApiSchema> result = Sut.Components.Schemas.GetSchemas(data.Value.OpenApi);

        // Assert
        result.Count.Should().Be(data.Value.expectedSchemaNames.Count);
    }

    [Fact]
    public void GetSchemas_Return_EmptyListWhenThereIsNoSchema()
    {
        // Arrange
        (OpenApiDocument OpenApi, List<string> expectedSchemaNames)? data = SchemasTestData.Get3SchemasTestData();
        OpenApiDocument document = data.Value.OpenApi;
        document.Components.Schemas = new Dictionary<string, OpenApiSchema>();

        // Act
        IDictionary<string, OpenApiSchema> result = Sut.Components.Schemas.GetSchemas(document);

        // Assert
        result.Count.Should().Be(0);
    }
}