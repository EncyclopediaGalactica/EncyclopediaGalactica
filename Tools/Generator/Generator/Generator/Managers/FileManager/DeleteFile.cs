namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.FileManager;

using Microsoft.Extensions.Logging;

public partial class FileManagerImpl
{
    /// <inheritdoc />
    public void DeleteFile(string pathToFile)
    {
        if (!string.IsNullOrEmpty(pathToFile)
            || !string.IsNullOrWhiteSpace(pathToFile))
        {
            if (CheckIfFileExist(pathToFile))
            {
                File.Delete(pathToFile);
                _logger.LogInformation("File is deleted: {Path}", pathToFile);
                return;
            }

            _logger.LogInformation("Path does not exist: {Path}", pathToFile);
            return;
        }

        _logger.LogInformation("Path is not provided: {Path}", pathToFile);
    }
}