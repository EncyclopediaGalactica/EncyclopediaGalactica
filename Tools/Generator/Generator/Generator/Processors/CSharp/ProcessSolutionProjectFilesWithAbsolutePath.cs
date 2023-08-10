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
            if (string.IsNullOrEmpty(item.BasePath)
                || string.IsNullOrWhiteSpace(item.BasePath)
                || string.IsNullOrEmpty(item.SolutionProjectFileWithType)
                || string.IsNullOrWhiteSpace(item.SolutionProjectFileWithType))
            {
                _logger.LogError(
                    "Either {S1} or {S2} is empty or null",
                    nameof(item.BasePath),
                    nameof(item.SolutionProjectFileWithType));
                throw new ArgumentException(
                    "Either basepath or solution project file with type is null or empty.");
            }

            item.ProjectFileWithAbsolutePath = _stringManager.ConcatDirectorySegments(
                item.BasePath,
                item.SolutionProjectFileWithType);
        });
    }
}