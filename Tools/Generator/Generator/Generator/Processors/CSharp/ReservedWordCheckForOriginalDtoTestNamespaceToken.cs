namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ReservedWordCheckForOriginalDtoTestNamespaceToken(
        List<TypeInfo> typeInfos,
        List<string> reservedWords)
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
            if (string.IsNullOrEmpty(typeInfo.OriginalDtoTestProjectNamespaceToken)
                || string.IsNullOrWhiteSpace(typeInfo.OriginalDtoTestProjectNamespaceToken))
            {
                continue;
            }

            if (reservedWords.Contains(typeInfo.OriginalDtoTestProjectNamespaceToken.ToLower()))
            {
                _logger.LogError("{OriginalDtoNamespaceToken} is reserved word",
                    typeInfo.OriginalDtoTestProjectNamespaceToken);
                throw new GeneratorException(
                    $"Original Dto namespace token, {typeInfo.OriginalDtoTestProjectNamespaceToken}" +
                    $" is reserved word.");
            }
        }
    }
}