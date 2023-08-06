namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.PathManager;

public partial class PathManagerImpl
{
    /// <inheritdoc />
    public string BuildPathStringWithFilename(string path, string fileName)
    {
        if (string.IsNullOrEmpty(path)
            || string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException(nameof(path));
        }

        if (string.IsNullOrEmpty(fileName)
            || string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentException(nameof(fileName));
        }

        path = CheckAndRemoveEndSlash(path);

        return $"{path}/{fileName}";
    }
}