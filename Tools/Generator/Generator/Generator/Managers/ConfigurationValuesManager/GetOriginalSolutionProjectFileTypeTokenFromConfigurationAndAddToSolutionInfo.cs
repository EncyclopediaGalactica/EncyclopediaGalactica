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
        if (!solutionInfo.ProjectInfos.Any())
        {
            _logger.LogError("There are no project infos populated");
            throw new GeneratorException("There are no project infos populated");
        }

        solutionInfo.ProjectInfos.ForEach(item =>
        {
            item.SolutionProjectFileType = generatorConfiguration.SolutionProjectFileType;
        });
    }
}