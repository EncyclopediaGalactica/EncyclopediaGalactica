namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ProcessSolutionProjectFilesWithAbsolutePath(SolutionInfo solutionInfo)
    {
        if (!solutionInfo.ProjectInfos.Any())
        {
            _logger.LogInformation("Solution info project infos list is empty");
            return;
        }

        foreach (ProjectInfo projectInfo in solutionInfo.ProjectInfos)
        {
            projectInfo.ProjectFileWithFullPath =
                _stringManager.ConcatDirectorySegments(
                    projectInfo.BasePath,
                    projectInfo.Name);
        }
    }
}