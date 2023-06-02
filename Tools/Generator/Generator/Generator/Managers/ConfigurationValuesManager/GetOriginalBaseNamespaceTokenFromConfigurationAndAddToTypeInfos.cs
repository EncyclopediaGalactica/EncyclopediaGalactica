namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.ConfigurationValuesManager;

using Configuration;
using Microsoft.Extensions.Logging;
using Models;

public partial class ConfigurationValuesManager
{
    /// <inheritdoc />
    public void GetOriginalBaseNamespaceTokenFromConfigurationAndAddToTypeInfos(
        List<TypeInfo> typeInfos,
        CodeGeneratorConfiguration? generatorConfiguration)
    {
        if (generatorConfiguration is null)
        {
            _logger.LogInformation("{GeneratorConfiguration} is null, skipping {Operation}",
                nameof(generatorConfiguration),
                nameof(GetOriginalBaseNamespaceTokenFromConfigurationAndAddToTypeInfos));
            return;
        }

        if (string.IsNullOrEmpty(generatorConfiguration.SolutionBaseNamespace)
            || string.IsNullOrWhiteSpace(generatorConfiguration.SolutionBaseNamespace))
        {
            _logger.LogInformation("{Param} is empty, skipping {Operation}",
                nameof(generatorConfiguration.SolutionBaseNamespace),
                nameof(GetOriginalBaseNamespaceTokenFromConfigurationAndAddToTypeInfos));
            return;
        }

        if (!typeInfos.Any())
        {
            _logger.LogInformation("{TypeInfos} list is empty, skipping {Operation}",
                nameof(typeInfos),
                nameof(GetOriginalBaseNamespaceTokenFromConfigurationAndAddToTypeInfos));
            return;
        }

        foreach (TypeInfo fileInfo in typeInfos)
        {
            fileInfo.OriginalBaseNamespaceToken = generatorConfiguration.SolutionBaseNamespace;
        }
    }
}