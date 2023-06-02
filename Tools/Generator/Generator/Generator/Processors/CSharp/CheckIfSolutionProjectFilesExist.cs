namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Microsoft.Extensions.Logging;
using Models;

public partial class CSharpProcessor
{
    /// <inheritdoc />
    public void CheckIfSolutionProjectFilesExist(SolutionInfo solutionInfo)
    {
        if (!solutionInfo.ProjectInfos.Any())
        {
            _logger.LogInformation("No available project info");
            return;
        }

        bool result = solutionInfo.ProjectInfos.All(
            p => _fileManager.CheckIfFileExist(p.ProjectFileWithFullPath));

        EvaluateCheckIfSolutionProjectFileExistsResult(result, solutionInfo);
    }

    private void EvaluateCheckIfSolutionProjectFileExistsResult(bool result, SolutionInfo solutionInfo)
    {
        if (!result)
        {
            solutionInfo.ProjectInfos.ForEach(p =>
            {
                if (!_fileManager.CheckIfFileExist(p.ProjectFileWithFullPath))
                {
                    _logger.LogError(
                        "{PATH} does not exist. Please, create it",
                        p.ProjectFileWithFullPath);
                    throw new GeneratorException(
                        $"{p.ProjectFileWithFullPath} does not exist. Please, create it!");
                }
            });
        }
    }
}