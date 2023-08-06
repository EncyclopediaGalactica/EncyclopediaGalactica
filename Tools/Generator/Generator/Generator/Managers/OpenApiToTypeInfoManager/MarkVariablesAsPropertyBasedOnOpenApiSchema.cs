namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.OpenApiToTypeInfoManager;

using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Models;

public partial class OpenApiToTypeInfoManager
{
    public void MarkVariablesAsPropertyBasedOnOpenApiSchema(
        List<TypeInfo> typeInfos,
        OpenApiDocument openApiYamlSchema)
    {
        if (!typeInfos.Any())
        {
            _logger.LogInformation("TypeInfos list is empty");
            return;
        }

        foreach (TypeInfo typeInfo in typeInfos)
        {
            if (!typeInfo.VariableInfos.Any())
            {
                _logger.LogInformation("{Type} does not have variables", typeInfo.OriginalTypeNameToken);
                continue;
            }

            if (string.IsNullOrEmpty(typeInfo.OriginalTypeNameToken)
                && string.IsNullOrWhiteSpace(typeInfo.OriginalTypeNameToken))
            {
                continue;
            }

            List<string> variableNames = _openApiDocumentManager.Components.Schemas
                .GetPropertyNamesBySchema(typeInfo.OriginalTypeNameToken, openApiYamlSchema);
            List<string> lowerCasedVariableNames = variableNames.Select(p => p.ToLower()).ToList();

            foreach (VariableInfo variableInfo in typeInfo.VariableInfos)
            {
                if (string.IsNullOrEmpty(variableInfo.OriginalVariableNameToken)
                    || string.IsNullOrWhiteSpace(variableInfo.OriginalVariableNameToken))
                {
                    continue;
                }

                if (lowerCasedVariableNames.Contains(variableInfo.OriginalVariableNameToken.ToLower()))
                {
                    variableInfo.IsProperty = true;
                }
            }
        }
    }
}