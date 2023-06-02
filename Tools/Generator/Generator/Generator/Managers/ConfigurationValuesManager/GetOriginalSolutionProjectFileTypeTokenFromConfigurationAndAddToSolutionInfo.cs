namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.ConfigurationValuesManager;

using Configuration;
using Models;

public partial class ConfigurationValuesManager
{
    /// <inheritdoc />
    public void GetOriginalSolutionProjectFileTypeTokenFromConfigurationAndAddToSolutionInfo(
        SolutionInfo solutionInfo,
        CodeGeneratorConfiguration generatorConfiguration)
    {
        solutionInfo.ProjectInfos.ForEach(item =>
        {
            item.SolutionProjectFileType = generatorConfiguration.SolutionProjectFileType;
        });
    }
}