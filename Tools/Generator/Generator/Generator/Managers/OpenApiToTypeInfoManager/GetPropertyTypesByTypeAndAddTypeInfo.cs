namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.OpenApiToTypeInfoManager;

using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Models;

public partial class OpenApiToTypeInfoManager
{
    /// <inheritdoc />
    public void GetPropertyTypesByTypeAndAddTypeInfo(List<TypeInfo> typeInfos, OpenApiDocument openApiDocument)
    {
        if (!typeInfos.Any())
        {
            _logger.LogInformation("No available type infos");
            return;
        }

        foreach (TypeInfo typeInfo in typeInfos)
        {
            if (string.IsNullOrEmpty(typeInfo.OriginalTypeNameToken)
                || string.IsNullOrWhiteSpace(typeInfo.OriginalTypeNameToken))
            {
                _logger.LogInformation("Original type name token is null, empty or whitespace");
                continue;
            }

            IDictionary<string, Dictionary<string, string>> propertyTypesBySchemas = _openApiDocumentManager.Components
                .Schemas.GetPropertyTypesBySchema(typeInfo.OriginalTypeNameToken, openApiDocument);

            if (!propertyTypesBySchemas.Any())
            {
                continue;
            }

            foreach (KeyValuePair<string, Dictionary<string, string>> keyValuePair in propertyTypesBySchemas)
            {
                VariableInfo variableInfo = typeInfo.VariableInfos
                    .First(p => p.OriginalVariableNameToken == keyValuePair.Key);
                variableInfo.OriginalVariableTypeNameToken = keyValuePair.Value["type"];
                variableInfo.OriginalVariableTypeFormat = keyValuePair.Value["format"];
            }
        }
    }
}