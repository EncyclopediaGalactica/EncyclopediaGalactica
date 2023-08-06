namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ProcessPropertiesByType(List<TypeInfo> typeInfos)
    {
        if (!typeInfos.Any())
        {
            _logger.LogInformation("Dto file infos is empty");
            return;
        }

        foreach (TypeInfo typeInfo in typeInfos)
        {
            if (!typeInfo.VariableInfos.Any())
            {
                continue;
            }

            foreach (VariableInfo propertyInfo in typeInfo.VariableInfos.Where(p => p.IsProperty))
            {
                if (string.IsNullOrEmpty(propertyInfo.OriginalVariableNameToken)
                    || string.IsNullOrWhiteSpace(propertyInfo.OriginalVariableNameToken))
                {
                    continue;
                }

                propertyInfo.VariableName = _stringManager.MakeSnakeCaseToPascalCase(
                    propertyInfo.OriginalVariableNameToken);
            }
        }
    }
}