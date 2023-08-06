namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.FileManager;

using Microsoft.Extensions.Logging;

public partial class FileManagerImpl
{
    /// <inheritdoc />
    public void CheckIfExistsOrCreate(string pathToFile)
    {
        if (string.IsNullOrEmpty(pathToFile)
            || string.IsNullOrWhiteSpace(pathToFile))
        {
            throw new ArgumentNullException(nameof(pathToFile));
        }

        if (CheckIfFileExist(pathToFile)) return;

        _logger.LogInformation("Path does not exist, it will be created. {Path}", pathToFile);
        File.Create(pathToFile);
    }
}