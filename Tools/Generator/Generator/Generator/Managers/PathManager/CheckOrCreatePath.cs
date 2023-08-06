namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.PathManager;

using Microsoft.Extensions.Logging;

public partial class PathManagerImpl
{
    /// <inheritdoc />
    public void CheckOrCreatePath(string path)
    {
        if (string.IsNullOrEmpty(path)
            || string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException(nameof(path));
        }

        path = CheckIfPathAbsoluteOrMakeItOne(path);

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            _logger.LogInformation("Directory does not exist, create one: {Path}", path);
        }
    }
}