namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.ConfigurationValuesManager;

using Configuration;
using Models;

public partial class ConfigurationValuesManager
{
    /// <inheritdoc />
    public void GetOriginalSolutionFileTypeTokenFromConfigurationAndAddToSolutionInfo(
        SolutionInfo solutionInfo,
        CodeGeneratorConfiguration generatorConfiguration)
    {
        solutionInfo.SolutionFileType = generatorConfiguration.SolutionFileType;
    }
}