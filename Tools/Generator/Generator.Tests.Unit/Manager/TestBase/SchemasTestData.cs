namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Tests.Unit.Manager.TestBase;

using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

public interface ISchemasTestData
{
    (OpenApiDocument openApi, List<string> expectedNames) GetSchemaNamesTestData();
    (OpenApiDocument openApi, List<string> schemaNames) Get3SchemasTestData();
    (OpenApiDocument openApi, List<string> schemaNames) Get1SchemaTestData();

    (OpenApiDocument openApi, Dictionary<string, List<string>> requiredPropertiesBySchemaName)
        GetRequiredPropertiesBySchemaTestData();

    (OpenApiDocument openApi, Dictionary<string, List<string>> schemaNames) GetPropertyNamesBySchemaTestData();

    (OpenApiDocument openApi, Dictionary<string,
            Dictionary<string, Dictionary<string, string>>> schemaWithPropertyNamesAndTypes)
        GetPropertyTypesBySchemaTestData();

    OpenApiDocument GetTestDataForMarkVariablesAsPropertyBasedOnOpenApiSchema();
}

[ExcludeFromCodeCoverage]
public class SchemasTestData : ISchemasTestData
{
    public (OpenApiDocument openApi, List<string> expectedNames) GetSchemaNamesTestData()
    {
        using FileStream yamlString = new FileStream(
            $"{Directory.GetCurrentDirectory()}/Manager/TestBase/GetSchemasTestData.yaml",
            FileMode.Open);
        OpenApiDocument openApi = new OpenApiStreamReader().Read(
            yamlString,
            out OpenApiDiagnostic? openApiDiagnostic);

        if (openApi is null)
        {
            throw new Exception("OpenApi document is null");
        }

        return (openApi, new List<string> { "errorModel", "newPet", "pet" });
    }

    public (OpenApiDocument openApi, List<string> schemaNames) Get3SchemasTestData()
    {
        using FileStream yamlString = new FileStream(
            $"{Directory.GetCurrentDirectory()}/Manager/TestBase/3_schemas.yaml",
            FileMode.Open);
        OpenApiDocument openApi = new OpenApiStreamReader().Read(
            yamlString,
            out OpenApiDiagnostic? openApiDiagnostic);

        if (openApi is null)
        {
            throw new Exception("OpenApi document is null");
        }

        return (openApi, new List<string> { "errorModel", "newPet", "pet" });
    }

    public (OpenApiDocument openApi, List<string> schemaNames) Get1SchemaTestData()
    {
        using FileStream yamlString = new FileStream(
            $"{Directory.GetCurrentDirectory()}/Manager/TestBase/3_schemas.yaml",
            FileMode.Open);
        OpenApiDocument openApi = new OpenApiStreamReader().Read(
            yamlString,
            out OpenApiDiagnostic? openApiDiagnostic);

        if (openApi is null)
        {
            throw new Exception("OpenApi document is null");
        }

        return (openApi, new List<string> { "errorModel", "newPet", "pet" });
    }

    public (OpenApiDocument openApi, Dictionary<string, List<string>> requiredPropertiesBySchemaName)
        GetRequiredPropertiesBySchemaTestData()
    {
        using FileStream yamlString = new FileStream(
            $"{Directory.GetCurrentDirectory()}/Manager/TestBase/required_properties_by_schema.yaml",
            FileMode.Open);
        OpenApiDocument openApi = new OpenApiStreamReader().Read(
            yamlString,
            out OpenApiDiagnostic? openApiDiagnostic);

        if (openApi is null)
        {
            throw new Exception("OpenApi document is null");
        }

        return (openApi, new Dictionary<string, List<string>>
        {
            { "noneRequired", new List<string>() },
            { "oneRequired", new List<string> { "name" } },
            { "twoRequired", new List<string> { "id", "name" } },
        });
    }

    public (OpenApiDocument openApi, Dictionary<string, List<String>> schemaNames) GetPropertyNamesBySchemaTestData()
    {
        using FileStream yamlString = new FileStream(
            $"{Directory.GetCurrentDirectory()}/Manager/TestBase/propertynames_by_schema.yaml",
            FileMode.Open);
        OpenApiDocument openApi = new OpenApiStreamReader().Read(
            yamlString,
            out OpenApiDiagnostic? openApiDiagnostic);

        if (openApi is null)
        {
            throw new Exception("OpenApi document is null");
        }

        Dictionary<string, List<string>> schemaNames = new Dictionary<string, List<string>>
        {
            { "oneProperty", new List<string> { "id" } },
            { "onePropertyWithAllRequired", new List<string> { "id" } },
            { "twoProperties", new List<string> { "id", "name" } },
            { "twoPropertiesWithAllRequired", new List<string> { "id", "name" } },
            { "threeProperties", new List<string> { "id", "name", "desc" } },
            { "threePropertiesWithAllRequired", new List<string> { "id", "name", "desc" } },
        };
        return (openApi, schemaNames);
    }

    public (OpenApiDocument openApi,
        Dictionary<string, Dictionary<string, Dictionary<string, string>>> schemaWithPropertyNamesAndTypes)
        GetPropertyTypesBySchemaTestData()
    {
        using FileStream yamlString = new FileStream(
            $"{Directory.GetCurrentDirectory()}/Manager/TestBase/propertytypenames_by_schema.yaml",
            FileMode.Open);
        OpenApiDocument openApi = new OpenApiStreamReader().Read(
            yamlString,
            out OpenApiDiagnostic? openApiDiagnostic);

        if (openApi is null)
        {
            throw new Exception("OpenApi document is null");
        }

        Dictionary<string, Dictionary<string, Dictionary<string, string>>> expectedData = new()
        {
            {
                "oneProperty", new Dictionary<string, Dictionary<string, string>>()
                {
                    {
                        "id", new Dictionary<string, string>
                        {
                            { "type", "integer" },
                            { "format", "int64" }
                        }
                    }
                }
            },
            {
                "onePropertyWithAllRequired", new Dictionary<string, Dictionary<string, string>>()
                {
                    {
                        "id", new Dictionary<string, string>
                        {
                            { "type", "integer" },
                            { "format", "int64" }
                        }
                    }
                }
            },
            {
                "twoProperties", new Dictionary<string, Dictionary<string, string>>()
                {
                    {
                        "id", new Dictionary<string, string>
                        {
                            { "type", "integer" },
                            { "format", "int64" }
                        }
                    },
                    {
                        "name", new Dictionary<string, string>
                        {
                            { "type", "string" },
                            { "format", "" }
                        }
                    },
                }
            },
            {
                "twoPropertiesWithAllRequired", new Dictionary<string, Dictionary<string, string>>()
                {
                    {
                        "id", new Dictionary<string, string>
                        {
                            { "type", "integer" },
                            { "format", "int64" }
                        }
                    },
                    {
                        "name", new Dictionary<string, string>
                        {
                            { "type", "string" },
                            { "format", "" }
                        }
                    },
                }
            },
            {
                "threeProperties", new Dictionary<string, Dictionary<string, string>>()
                {
                    {
                        "id", new Dictionary<string, string>
                        {
                            { "type", "integer" },
                            { "format", "int64" }
                        }
                    },
                    {
                        "name", new Dictionary<string, string>
                        {
                            { "type", "string" },
                            { "format", "" }
                        }
                    },
                    {
                        "desc", new Dictionary<string, string>
                        {
                            { "type", "string" },
                            { "format", "" }
                        }
                    },
                }
            },
            {
                "threePropertiesWithAllRequired", new Dictionary<string, Dictionary<string, string>>()
                {
                    {
                        "id", new Dictionary<string, string>
                        {
                            { "type", "integer" },
                            { "format", "int64" }
                        }
                    },
                    {
                        "name", new Dictionary<string, string>
                        {
                            { "type", "string" },
                            { "format", "" }
                        }
                    },
                    {
                        "desc", new Dictionary<string, string>
                        {
                            { "type", "string" },
                            { "format", "" }
                        }
                    },
                }
            },
        };

        return (openApi, expectedData);
    }

    public OpenApiDocument GetTestDataForMarkVariablesAsPropertyBasedOnOpenApiSchema()
    {
        using FileStream yamlString = new FileStream(
            $"{Directory.GetCurrentDirectory()}/Manager/TestBase/mark_properties_3_schemas.yaml",
            FileMode.Open);
        OpenApiDocument openApi = new OpenApiStreamReader().Read(
            yamlString,
            out OpenApiDiagnostic? openApiDiagnostic);
        return openApi;
    }
}