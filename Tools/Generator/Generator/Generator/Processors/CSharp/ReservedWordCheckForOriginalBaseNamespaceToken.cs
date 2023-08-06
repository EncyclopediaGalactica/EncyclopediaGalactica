namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ReservedWordCheckForOriginalBaseNamespaceToken(List<TypeInfo> typeInfos, List<string> reservedWords)
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
            if (string.IsNullOrEmpty(typeInfo.OriginalBaseNamespaceToken)
                || string.IsNullOrWhiteSpace(typeInfo.OriginalBaseNamespaceToken))
            {
                continue;
            }

            if (typeInfo.OriginalBaseNamespaceToken.Contains("."))
            {
                string[] chunks = typeInfo.OriginalBaseNamespaceToken.Split(".");
                foreach (string chunk in chunks)
                {
                    if (string.IsNullOrEmpty(chunk) || string.IsNullOrWhiteSpace(chunk))
                    {
                        continue;
                    }

                    if (reservedWords.Contains(chunk.ToLower()))
                    {
                        _logger.LogError("The provided base namespace token, " +
                                         "{OriginalBaseNamespaceToken} includes a reserved word",
                            typeInfo.OriginalBaseNamespaceToken);
                        throw new GeneratorException($"The provided base namespace token, " +
                                                     $"{typeInfo.OriginalBaseNamespaceToken} includes a " +
                                                     $"reserved word");
                    }
                }
            }

            if (reservedWords.Contains(typeInfo.OriginalBaseNamespaceToken.ToLower()))
            {
                _logger.LogError("The provided base namespace token, " +
                                 "{OriginalBaseNamespaceToken} is reserved word",
                    typeInfo.OriginalBaseNamespaceToken);
                throw new GeneratorException($"The provided base namespace token, " +
                                             $"{typeInfo.OriginalBaseNamespaceToken} is reserved word");
            }
        }
    }
}