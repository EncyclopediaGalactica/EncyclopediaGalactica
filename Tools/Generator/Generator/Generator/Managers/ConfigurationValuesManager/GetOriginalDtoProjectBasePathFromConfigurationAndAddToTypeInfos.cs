namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.ConfigurationValuesManager;

using Configuration;
using Microsoft.Extensions.Logging;
using Models;

public partial class ConfigurationValuesManager
{
    /// <inheritdoc />
    public void GetOriginalDtoProjectBasePathFromConfigurationAndAddToTypeInfos(
        List<TypeInfo> typeInfos,
        CodeGeneratorConfiguration? generatorConfiguration)
    {
        if (generatorConfiguration is null)
        {
            _logger.LogInformation("{GeneratorConfigurationInstance} is null", nameof(generatorConfiguration));
            return;
        }

        if (string.IsNullOrEmpty(generatorConfiguration.DtoProjectBasePath)
            || string.IsNullOrWhiteSpace(generatorConfiguration.DtoProjectBasePath))
        {
            _logger.LogInformation("{DtoProjectAdditionalPath} is null, empty or whitespace, skipping",
                nameof(generatorConfiguration.DtoProjectBasePath));
            return;
        }

        if (!typeInfos.Any())
        {
            _logger.LogInformation("{FileInfos} is empty, skipping {Operation}",
                nameof(typeInfos),
                nameof(GetOriginalDtoAdditionalPathFromConfigurationAndAddToTypeInfo));
            return;
        }

        foreach (TypeInfo fileInfo in typeInfos)
        {
            fileInfo.OriginalDtoProjectBasePathToken = generatorConfiguration.DtoProjectBasePath;
        }
    }
}