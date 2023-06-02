namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void TypeCheckInGenerationScope(List<TypeInfo> typeInfos, List<string> typesInGenerationScope)
    {
        if (!typeInfos.Any())
        {
            _logger.LogInformation("Dto type infos list is empty");
            return;
        }

        if (!typesInGenerationScope.Any())
        {
            _logger.LogInformation("Types in generation scope list is empty");
            return;
        }

        foreach (TypeInfo typeInfo in typeInfos)
        {
            if (string.IsNullOrEmpty(typeInfo.TypeName) || string.IsNullOrWhiteSpace(typeInfo.TypeName))
            {
                _logger.LogInformation(
                    "{OrigName} does not have processed type name yet. Probably it is not processed yet",
                    typeInfo.OriginalTypeNameToken);
                continue;
            }

            if (typesInGenerationScope.Contains(typeInfo.TypeName.ToLower()))
            {
                _logger.LogInformation("{TypeName} is already in use", typeInfo.TypeName);
                throw new GeneratorException($"{typeInfo.TypeName} is already in use.");
            }
        }
    }
}