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

        solutionInfo.ProjectInfos.ForEach(item =>
        {
            string baseAndName = _stringManager.ConcatDirectorySegments(
                item.BasePath,
                item.Name);
            item.ProjectFileWithFullPath = _stringManager.ConcatDirectorySegments(
                baseAndName,
                item.SolutionProjectFileWithType);
        });
    }
}