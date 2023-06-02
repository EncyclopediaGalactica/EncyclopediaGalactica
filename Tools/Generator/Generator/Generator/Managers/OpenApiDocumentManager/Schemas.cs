namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.OpenApiDocumentManager;

using Microsoft.OpenApi.Models;

/// <inheritdoc />
public class Schemas : ISchemas
{
    /// <inheritdoc />
    public List<string> GetSchemaNames(Microsoft.OpenApi.Models.OpenApiDocument openApiDocument)
    {
        if (!openApiDocument.Components.Schemas.Any())
        {
            throw new ArgumentException("Openapi schema does not any components.schemas");
        }

        return openApiDocument.Components.Schemas.Select(p => p.Key).ToList();
    }

    /// <inheritdoc />
    public IDictionary<string, OpenApiSchema> GetSchemas(OpenApiDocument openApiDocument)
    {
        if (!openApiDocument.Components.Schemas.Any())
        {
            return new Dictionary<string, OpenApiSchema>();
        }

        return openApiDocument.Components.Schemas;
    }

    /// <inheritdoc />
    public List<string> GetRequiredPropertiesBySchema(string schemaName, OpenApiDocument openApiDocument)
    {
        if (string.IsNullOrEmpty(schemaName) || string.IsNullOrWhiteSpace(schemaName))
        {
            throw new ArgumentException(nameof(schemaName));
        }

        IDictionary<string, OpenApiSchema> schemas = GetSchemas(openApiDocument);

        if (!schemas.Any() || schemas.Where(p => p.Key == schemaName).ToList().Count == 0)
        {
            return new List<string>();
        }

        return schemas.First(p => p.Key == schemaName).Value.Required.ToList();
    }

    /// <inheritdoc />
    public List<string> GetPropertyNamesBySchema(string schemaName, OpenApiDocument openApiDocument)
    {
        if (string.IsNullOrEmpty(schemaName) || string.IsNullOrWhiteSpace(schemaName))
        {
            throw new ArgumentException(nameof(schemaName));
        }

        IDictionary<string, OpenApiSchema> schemas = GetSchemas(openApiDocument);

        if (!schemas.Any() || schemas.Where(p => p.Key == schemaName).ToList().Count == 0)
        {
            return new List<string>();
        }

        return schemas.First(p => p.Key == schemaName).Value.Properties.Keys.ToList();
    }

    /// <inheritdoc />
    public IDictionary<string, Dictionary<string, string>> GetPropertyTypesBySchema(
        string schemaName,
        OpenApiDocument openApiDocument)
    {
        if (string.IsNullOrEmpty(schemaName) || string.IsNullOrWhiteSpace(schemaName))
        {
            throw new ArgumentException(nameof(schemaName));
        }

        IDictionary<string, OpenApiSchema> schemas = GetSchemas(openApiDocument);
        OpenApiSchema schema = schemas.First(p => p.Key == schemaName).Value;

        Dictionary<string, Dictionary<string, string>> propertyNamesAndTheirType = new();

        foreach (KeyValuePair<string, OpenApiSchema> property in schema.Properties)
        {
            Dictionary<string, string> propertyInfo = new();
            propertyInfo.Add("type", property.Value.Type);
            if (string.IsNullOrEmpty(property.Value.Format) || string.IsNullOrWhiteSpace(property.Value.Format))
            {
                propertyInfo.Add("format", string.Empty);
            }
            else
            {
                propertyInfo.Add("format", property.Value.Format);
            }

            propertyNamesAndTheirType.Add(property.Key, propertyInfo);
        }

        return propertyNamesAndTheirType;
    }
}