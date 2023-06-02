namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ProcessDtoTestNamespace(List<TypeInfo> typeInfos)
    {
        if (!typeInfos.Any())
        {
            _logger.LogInformation("No available dto file infos");
            return;
        }

        foreach (TypeInfo fileInfo in typeInfos)
        {
            fileInfo.Namespace = _stringManager.MakeUppercaseTheCharAfterTheDot(
                _stringManager.ConcatCsharpNamespaceTokens(
                    fileInfo.OriginalBaseNamespaceToken,
                    fileInfo.OriginalDtoTestProjectNamespaceToken));
        }
    }
}