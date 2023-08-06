namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ProcessSolutionNameWithFullPath(SolutionInfo solutionInfo)
    {
        if (string.IsNullOrEmpty(solutionInfo.Name) || string.IsNullOrWhiteSpace(solutionInfo.Name))
        {
            _logger.LogInformation("No solution name");
            return;
        }

        if (string.IsNullOrEmpty(solutionInfo.BaseAbsolutePath) ||
            string.IsNullOrWhiteSpace(solutionInfo.BaseAbsolutePath))
        {
            _logger.LogInformation("No path for solution");
            return;
        }

        solutionInfo.SolutionFileWithFullPath = _stringManager.ConcatDirectorySegments(
            _pathManager.CheckIfPathAbsoluteOrMakeItOne(solutionInfo.BaseAbsolutePath),
            solutionInfo.SolutionNameWithFileExtension);
    }
}