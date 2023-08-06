namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    public void ReservedWordCheckForOriginalDtoNamespaceToken(List<TypeInfo> typeInfos, List<string> reservedWords)
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
            if (string.IsNullOrEmpty(typeInfo.OriginalDtoNamespaceToken)
                || string.IsNullOrWhiteSpace(typeInfo.OriginalDtoNamespaceToken))
            {
                continue;
            }

            if (reservedWords.Contains(typeInfo.OriginalDtoNamespaceToken.ToLower()))
            {
                _logger.LogError("{OriginalDtoNamespaceToken} is reserved word", typeInfo.OriginalDtoNamespaceToken);
                throw new GeneratorException($"Original Dto namespace token, {typeInfo.OriginalDtoNamespaceToken}" +
                                             $" is reserved word.");
            }
        }
    }
}