namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void AddTypeNamesToGenerationScope(List<TypeInfo> typeInfos, List<string> typesInGenerationScope)
    {
        if (!typeInfos.Any())
        {
            _logger.LogInformation("Type info list is empty");
            return;
        }

        if (!typesInGenerationScope.Any())
        {
            _logger.LogInformation("Types in generating scope is empty");
            return;
        }

        foreach (TypeInfo typeInfo in typeInfos)
        {
            if (string.IsNullOrEmpty(typeInfo.TypeName) || string.IsNullOrWhiteSpace(typeInfo.TypeName))
            {
                _logger.LogInformation("{Orig} does not have type name. Possibly not processed yet",
                    typeInfo.OriginalTypeNameToken);
                continue;
            }

            typesInGenerationScope.Add(typeInfo.TypeName);
        }
    }
}