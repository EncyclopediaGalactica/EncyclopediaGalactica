namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.PathManager;

public partial class PathManagerImpl
{
    public string BuildPathString(string path1, string path2)
    {
        return ConcatenatePathSegments(path1, path2);
    }

    /// <inheritdoc />
    public string BuildPathString(string path1, string path2, string path3)
    {
        return ConcatenatePathSegments(path1, path2, path3);
    }
}