namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void CheckIfSolutionPathExists(SolutionInfo solutionInfo)
    {
        if (string.IsNullOrEmpty(solutionInfo.BaseAbsolutePath)
            || string.IsNullOrWhiteSpace(solutionInfo.BaseAbsolutePath))
        {
            _logger.LogInformation("{Value} is missing", nameof(solutionInfo.BaseAbsolutePath));
            return;
        }

        if (!_pathManager.IsPathExists(solutionInfo.BaseAbsolutePath))
        {
            _logger.LogError("{Path} solution base path does not exist. Please create it",
                solutionInfo.BaseAbsolutePath);
            throw new GeneratorException(
                $"{solutionInfo.BaseAbsolutePath} does not exist, please create it.");
        }
    }
}