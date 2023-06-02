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

        bool result = solutionInfo.ProjectInfos
            .All(p => _pathManager.IsPathExists(p.BasePath));

        EvaluateCheckSolutionProjectsPathExistResult(solutionInfo, result);
    }

    private void EvaluateCheckSolutionProjectsPathExistResult(SolutionInfo solutionInfo, bool result)
    {
        if (!result)
        {
            solutionInfo.ProjectInfos.ForEach(p =>
            {
                if (!_pathManager.IsPathExists(p.BasePath))
                {
                    _logger.LogInformation(
                        "{Path} does not exist, please create it",
                        p.BasePath);
                    throw new GeneratorException(
                        $"{p.BasePath} does not exist, please create it.");
                }
            });
        }
    }
}