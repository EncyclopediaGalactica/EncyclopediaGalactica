namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void ProcessSolutionBasePath(SolutionInfo solutionInfo)
    {
        if (string.IsNullOrEmpty(solutionInfo.OriginalBasePathToken)
            || string.IsNullOrWhiteSpace(solutionInfo.OriginalBasePathToken))
        {
            _logger.LogInformation("{Path} is empty", nameof(solutionInfo.OriginalBasePathToken));
        }

        solutionInfo.BaseAbsolutePath = _pathManager.CheckIfPathAbsoluteOrMakeItOne(solutionInfo.OriginalBasePathToken);
    }
}