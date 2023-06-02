namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.OpenApiToTypeInfoManager;

using Microsoft.OpenApi.Models;
using Models;

public partial class OpenApiToTypeInfoManager
{
    /// <inheritdoc />
    public void GetTypeNamesFromOpenApiAndAddToTypeInfo(
        List<TypeInfo> typeInfos,
        OpenApiDocument openApiDocument)
    {
        if (typeInfos.Any())
        {
            throw new GeneratorException("File infos list is not empty.");
        }

        List<string> typeNames = _openApiDocumentManager.Components.Schemas.GetSchemaNames(openApiDocument);

        foreach (string typeName in typeNames)
        {
            typeInfos.Add(new TypeInfo
            {
                OriginalTypeNameToken = typeName
            });
        }
    }
}