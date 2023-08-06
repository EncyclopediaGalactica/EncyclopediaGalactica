namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.FileManager;

using Microsoft.Extensions.Logging;

public partial class FileManagerImpl
{
    /// <inheritdoc />
    public string ReadAllText(string pathToFile)
    {
        if (string.IsNullOrEmpty(pathToFile)
            || string.IsNullOrWhiteSpace(pathToFile))
        {
            throw new ArgumentNullException(nameof(pathToFile));
        }

        string? result = File.ReadAllText(pathToFile);

        if (string.IsNullOrEmpty(result) || string.IsNullOrWhiteSpace(result))
        {
            _logger.LogInformation(
                "Reading file resulted empty or null. Filepath: {PathToFile}", pathToFile);
        }

        return result;
    }
}