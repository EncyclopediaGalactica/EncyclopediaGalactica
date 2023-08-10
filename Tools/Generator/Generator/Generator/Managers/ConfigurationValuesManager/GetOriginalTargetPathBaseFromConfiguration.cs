namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.ConfigurationValuesManager;

using Configuration;
using Microsoft.Extensions.Logging;
using Models;

public partial class ConfigurationValuesManager
{
    /// <inheritdoc />
    public void GetOriginalTargetPathBaseFromConfiguration(
        List<TypeInfo> typeInfos,
        CodeGeneratorConfiguration? generatorConfiguration)
    {
        if (generatorConfiguration is null)
        {
            _logger.LogInformation("{GeneratorConfiguration} is null, skipping {Operation}",
                nameof(generatorConfiguration),
                nameof(GetOriginalTargetPathBaseFromConfiguration));
            return;
        }

        if (string.IsNullOrEmpty(generatorConfiguration.TargetDirectory)
            || string.IsNullOrWhiteSpace(generatorConfiguration.TargetDirectory))
        {
            _logger.LogInformation("{Param} is empty, skipping {Operation}",
                nameof(generatorConfiguration.TargetDirectory),
                nameof(GetOriginalTargetPathBaseFromConfiguration));
            return;
        }

        if (!typeInfos.Any())
        {
            _logger.LogInformation("{TypeInfos} list is empty, skipping {Operation}",
                nameof(typeInfos),
                nameof(GetOriginalTargetPathBaseFromConfiguration));
            return;
        }

        foreach (TypeInfo fileInfo in typeInfos)
        {
            fileInfo.OriginalTargetDirectoryToken = generatorConfiguration.TargetDirectory;
        }
    }

    public void GetOriginalTargetPathBaseFromConfiguration(
        SolutionInfo solutionInfo,
        CodeGeneratorConfiguration? generatorConfiguration)
    {
        if (generatorConfiguration is null)
        {
            _logger.LogInformation("{GeneratorConfiguration} is null, skipping {Operation}",
                nameof(generatorConfiguration),
                nameof(GetOriginalTargetPathBaseFromConfiguration));
            return;
        }

        if (string.IsNullOrEmpty(generatorConfiguration.TargetDirectory)
            || string.IsNullOrWhiteSpace(generatorConfiguration.TargetDirectory))
        {
            _logger.LogInformation("{Param} is empty, skipping {Operation}",
                nameof(generatorConfiguration.TargetDirectory),
                nameof(GetOriginalTargetPathBaseFromConfiguration));
            return;
        }

        solutionInfo.OriginalBasePathToken = generatorConfiguration.TargetDirectory;
    }
}