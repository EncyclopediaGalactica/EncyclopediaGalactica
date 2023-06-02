namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.ConfigurationValuesManager;

using Configuration;
using Microsoft.Extensions.Logging;
using Models;

public partial class ConfigurationValuesManager
{
    /// <inheritdoc />
    public void GetOriginalDtoProjectNamespaceTokenFromConfigurationAndAddToTypeInfos(
        List<TypeInfo> typeInfos,
        CodeGeneratorConfiguration? generatorConfiguration)
    {
        if (generatorConfiguration is null)
        {
            _logger.LogInformation("{GeneratorConfiguration} is null, skipping {Operation}",
                nameof(generatorConfiguration),
                nameof(GetOriginalDtoProjectNamespaceTokenFromConfigurationAndAddToTypeInfos));
            return;
        }

        if (string.IsNullOrEmpty(generatorConfiguration.DtoProjectNameSpace)
            || string.IsNullOrWhiteSpace(generatorConfiguration.DtoProjectNameSpace))
        {
            _logger.LogInformation("{Param} is empty, skipping {Operation}",
                nameof(generatorConfiguration.DtoProjectNameSpace),
                nameof(GetOriginalDtoProjectNamespaceTokenFromConfigurationAndAddToTypeInfos));
            return;
        }

        if (!typeInfos.Any())
        {
            _logger.LogInformation("{TypeInfos} list is empty, skipping {Operation}",
                nameof(typeInfos),
                nameof(GetOriginalDtoProjectNamespaceTokenFromConfigurationAndAddToTypeInfos));
            return;
        }

        foreach (TypeInfo fileInfo in typeInfos)
        {
            fileInfo.OriginalDtoNamespaceToken = generatorConfiguration.DtoProjectNameSpace;
        }
    }
}