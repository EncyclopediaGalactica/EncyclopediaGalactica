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

        solutionInfo.ProjectInfos.ForEach(item =>
        {
            if (!_fileManager.CheckIfFileExist(item.ProjectFileWithFullPath))
            {
                _logger.LogError(
                    "{PATH} path does not exist. Please, create it",
                    item.ProjectFileWithFullPath);
                throw new GeneratorException(
                    $"{item.ProjectFileWithFullPath} path does not exist. Please, create it!");
            }
        });
    }
}