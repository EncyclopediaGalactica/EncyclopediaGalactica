namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    public void ProcessTypeName(List<TypeInfo> typeInfos, string? typeNamePostfix)
    {
        if (!typeInfos.Any())
        {
            _logger.LogInformation("Dto file infos is empty");
            return;
        }

        if (string.IsNullOrEmpty(typeNamePostfix)
            || string.IsNullOrWhiteSpace(typeNamePostfix))
        {
            _logger.LogInformation("Typename postfix is empty, null or whitespace");
        }

        foreach (TypeInfo fileInfo in typeInfos)
        {
            string typeName = _stringManager.Concat(
                _stringManager.MakeFirstCharUpperCase(fileInfo.OriginalTypeNameToken),
                typeNamePostfix
            );
            fileInfo.TypeName = typeName;
        }
    }
}