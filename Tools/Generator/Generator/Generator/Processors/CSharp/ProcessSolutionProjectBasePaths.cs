namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ProcessSolutionProjectBasePaths(SolutionInfo solutionInfo)
    {
        if (string.IsNullOrEmpty(solutionInfo.BaseAbsolutePath) ||
            string.IsNullOrWhiteSpace(solutionInfo.BaseAbsolutePath))
        {
            _logger.LogInformation("{Path} is empty", nameof(solutionInfo.BaseAbsolutePath));
            return;
        }

        if (!solutionInfo.ProjectInfos.Any())
        {
            _logger.LogInformation("{ProjectInfos} is empty", nameof(solutionInfo.ProjectInfos));
            return;
        }

        foreach (ProjectInfo projectInfo in solutionInfo.ProjectInfos)
        {
            string projectPath = _stringManager.ConcatDirectorySegments(
                solutionInfo.BaseAbsolutePath,
                projectInfo.BasePath);
            projectInfo.BasePath = projectPath;
        }
    }
}