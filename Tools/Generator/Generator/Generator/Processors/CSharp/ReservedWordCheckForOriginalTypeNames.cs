namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    public void ReservedWordCheckForOriginalTypeNames(List<TypeInfo> typeInfos, List<string> reservedWords)
    {
        if (!typeInfos.Any())
        {
            _logger.LogInformation("Dto file infos is empty");
            return;
        }

        if (!reservedWords.Any())
        {
            _logger.LogInformation("List of reserved is empty");
            return;
        }

        foreach (TypeInfo typeInfo in typeInfos)
        {
            if (string.IsNullOrEmpty(typeInfo.OriginalTypeNameToken)
                || string.IsNullOrWhiteSpace(typeInfo.OriginalTypeNameToken))
            {
                continue;
            }

            if (reservedWords.Contains(typeInfo.OriginalTypeNameToken.ToLower()))
            {
                _logger.LogError("Type name, {TypeName} is a reserved word", typeInfo.OriginalTypeNameToken);
                throw new GeneratorException($"Type name, {typeInfo.OriginalTypeNameToken}, is a " +
                                             $"reserved word.");
            }
        }
    }
}