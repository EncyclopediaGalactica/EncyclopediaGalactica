namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.ConfigurationValuesManager;

using Configuration;
using Microsoft.Extensions.Logging;
using Models;

public partial class ConfigurationValuesManager
{
    /// <inheritdoc />
    public void GetOriginalSolutionNameTokenFromConfiguration(SolutionInfo solutionInfo,
        CodeGeneratorConfiguration? generatorConfiguration)
    {
        if (generatorConfiguration is null)
        {
            _logger.LogInformation("{GeneratorConfigurationInstance} is null", nameof(generatorConfiguration));
            return;
        }

        if (string.IsNullOrEmpty(generatorConfiguration.SolutionName)
            || string.IsNullOrWhiteSpace(generatorConfiguration.SolutionName))
        {
            _logger.LogInformation("{DtoProjectAdditionalPath} is null, empty or whitespace, skipping",
                nameof(generatorConfiguration.SolutionName));
            return;
        }

        solutionInfo.OriginalNameToken = generatorConfiguration.SolutionName;
    }
}