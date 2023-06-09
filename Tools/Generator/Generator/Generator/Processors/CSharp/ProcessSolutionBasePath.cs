namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ProcessSolutionBasePath(SolutionInfo solutionInfo)
    {
        if (string.IsNullOrEmpty(solutionInfo.OriginalTargetDirectoryToken)
            || string.IsNullOrWhiteSpace(solutionInfo.OriginalTargetDirectoryToken))
        {
            _logger.LogInformation("{Path} is empty", nameof(solutionInfo.OriginalTargetDirectoryToken));
        }

        solutionInfo.BaseAbsolutePath = _pathManager.CheckIfPathAbsoluteOrMakeItOne(solutionInfo.OriginalTargetDirectoryToken);
    }
}