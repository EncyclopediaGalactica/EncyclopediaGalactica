namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.ConfigurationValuesManager;

using Configuration;
using Microsoft.Extensions.Logging;
using Models;

public partial class ConfigurationValuesManager
{
    /// <inheritdoc />
    public void GetOriginalTargetDirectoryTokenFromConfiguration(
        List<TypeInfo> typeInfos,
        CodeGeneratorConfiguration? generatorConfiguration)
    {
        if (generatorConfiguration is null)
        {
            _logger.LogInformation("{GeneratorConfiguration} is null, skipping {Operation}",
                nameof(generatorConfiguration),
                nameof(GetOriginalTargetDirectoryTokenFromConfiguration));
            return;
        }

        if (string.IsNullOrEmpty(generatorConfiguration.TargetDirectory)
            || string.IsNullOrWhiteSpace(generatorConfiguration.TargetDirectory))
        {
            _logger.LogInformation("{Param} is empty, skipping {Operation}",
                nameof(generatorConfiguration.TargetDirectory),
                nameof(GetOriginalTargetDirectoryTokenFromConfiguration));
            return;
        }

        if (!typeInfos.Any())
        {
            _logger.LogInformation("{TypeInfos} list is empty, skipping {Operation}",
                nameof(typeInfos),
                nameof(GetOriginalTargetDirectoryTokenFromConfiguration));
            return;
        }

        foreach (TypeInfo fileInfo in typeInfos)
        {
            fileInfo.OriginalTargetDirectoryToken = generatorConfiguration.TargetDirectory;
        }
    }

    public void GetOriginalTargetDirectoryTokenFromConfiguration(
        SolutionInfo solutionInfo,
        CodeGeneratorConfiguration? generatorConfiguration)
    {
        if (generatorConfiguration is null)
        {
            _logger.LogInformation("{GeneratorConfiguration} is null, skipping {Operation}",
                nameof(generatorConfiguration),
                nameof(GetOriginalTargetDirectoryTokenFromConfiguration));
            return;
        }

        if (string.IsNullOrEmpty(generatorConfiguration.TargetDirectory)
            || string.IsNullOrWhiteSpace(generatorConfiguration.TargetDirectory))
        {
            _logger.LogInformation("{Param} is empty, skipping {Operation}",
                nameof(generatorConfiguration.TargetDirectory),
                nameof(GetOriginalTargetDirectoryTokenFromConfiguration));
            return;
        }

        solutionInfo.OriginalTargetDirectoryToken = generatorConfiguration.TargetDirectory;
    }
}