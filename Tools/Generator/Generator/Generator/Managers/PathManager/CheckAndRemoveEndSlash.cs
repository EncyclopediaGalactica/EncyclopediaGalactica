namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.PathManager;

public partial class PathManagerImpl
{
    /// <inheritdoc />
    public string CheckAndRemoveEndSlash(string path)
    {
        if (string.IsNullOrEmpty(path)
            || string.IsNullOrWhiteSpace(path))
        {
            return path;
        }

        if (path[^1].ToString() == "/")
        {
            return path[..^1];
        }

        return path;
    }
}