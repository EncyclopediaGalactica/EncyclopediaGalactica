namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.ConfigurationValuesManager;

using Configuration;
using Microsoft.Extensions.Logging;
using Models;

public partial class ConfigurationValuesManager
{
    /// <inheritdoc />
    public void GetOriginalSolutionProjectFileTypeTokenFromConfigurationAndAddToSolutionInfo(
        SolutionInfo solutionInfo,
        CodeGeneratorConfiguration generatorConfiguration)
    {
        if (!string.IsNullOrEmpty(generatorConfiguration.SolutionProjectFileType)
            || !string.IsNullOrWhiteSpace(generatorConfiguration.SolutionProjectFileType))
        {
            solutionInfo.SolutionProjectFileType = generatorConfiguration.SolutionProjectFileType;
        }
    }
}