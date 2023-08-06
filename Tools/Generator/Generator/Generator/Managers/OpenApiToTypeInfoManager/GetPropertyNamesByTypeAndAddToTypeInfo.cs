namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.OpenApiToTypeInfoManager;

using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Models;

public partial class OpenApiToTypeInfoManager
{
    /// <inheritdoc />
    public void GetPropertyNamesByTypeAndAddToTypeInfo(List<TypeInfo> typeInfos, OpenApiDocument openApiDocument)
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

            List<string> propertyNamesBySchema = _openApiDocumentManager.Components.Schemas.GetPropertyNamesBySchema(
                typeInfo.OriginalTypeNameToken,
                openApiDocument);

            if (!propertyNamesBySchema.Any())
            {
                continue;
            }

            List<VariableInfo> propertyInfos = new List<VariableInfo>();
            foreach (string property in propertyNamesBySchema)
            {
                propertyInfos.Add(new VariableInfo
                {
                    OriginalVariableNameToken = property
                });
            }

            typeInfo.VariableInfos = propertyInfos;
        }
    }
}