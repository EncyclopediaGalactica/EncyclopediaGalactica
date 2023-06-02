namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void CheckIfSolutionFileExists(SolutionInfo solutionInfo)
    {
        if (!_fileManager.CheckIfFileExist(solutionInfo.SolutionFileWithFullPath))
        {
            _logger.LogError("{Solution} does not exist", solutionInfo.SolutionFileWithFullPath);
            throw new GeneratorException(
                $"{solutionInfo.SolutionFileWithFullPath} does not exist.");
        }
    }
}