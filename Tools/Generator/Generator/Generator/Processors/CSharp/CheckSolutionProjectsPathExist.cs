namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void CheckIfSolutionProjectsPathExist(SolutionInfo solutionInfo)
    {
        if (!solutionInfo.ProjectInfos.Any())
        {
            _logger.LogInformation("No available project infos");
            return;
        }

        solutionInfo.ProjectInfos.ForEach(item =>
        {
            if (!_pathManager.IsPathExists(item.BasePath))
            {
                _logger.LogInformation(
                    "{Path} does not exist, please create it",
                    item.BasePath);
                throw new GeneratorException(
                    $"{item.BasePath} does not exist, please create it.");
            }
        });
    }
}