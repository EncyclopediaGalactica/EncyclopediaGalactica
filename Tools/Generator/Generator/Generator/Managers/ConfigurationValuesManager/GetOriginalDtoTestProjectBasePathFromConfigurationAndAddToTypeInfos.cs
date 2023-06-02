namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.ConfigurationValuesManager;

using Configuration;
using Microsoft.Extensions.Logging;
using Models;

public partial class ConfigurationValuesManager
{
    /// <inheritdoc />
    public void GetOriginalDtoTestProjectBasePathFromConfigurationAndAddToTypeInfos(List<TypeInfo> typeInfos,
        CodeGeneratorConfiguration? generatorConfiguration)
    {
        if (generatorConfiguration is null)
        {
            _logger.LogInformation("{GeneratorConfiguration} is null, skipping {Operation}",
                nameof(generatorConfiguration),
                nameof(GetOriginalDtoTestProjectBasePathFromConfigurationAndAddToTypeInfos));
            return;
        }

        if (string.IsNullOrEmpty(generatorConfiguration.DtoProjectTestUnitBasePath)
            || string.IsNullOrWhiteSpace(generatorConfiguration.DtoProjectTestUnitBasePath))
        {
            _logger.LogInformation("{Param} is empty, skipping {Operation}",
                nameof(generatorConfiguration.DtoProjectTestUnitBasePath),
                nameof(GetOriginalDtoTestProjectBasePathFromConfigurationAndAddToTypeInfos));
            return;
        }

        if (!typeInfos.Any())
        {
            _logger.LogInformation("{TypeInfos} list is empty, skipping {Operation}",
                nameof(typeInfos),
                nameof(GetOriginalDtoTestProjectBasePathFromConfigurationAndAddToTypeInfos));
            return;
        }

        foreach (TypeInfo typeInfo in typeInfos)
        {
            typeInfo.OriginalDtoTestProjectBasePathToken = generatorConfiguration.DtoProjectTestUnitBasePath;
        }
    }
}