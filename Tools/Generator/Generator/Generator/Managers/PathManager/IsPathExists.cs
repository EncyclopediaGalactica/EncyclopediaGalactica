namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.PathManager;

public partial class PathManagerImpl
{
    /// <inheritdoc />
    public bool IsPathExists(string path)
    {
        if (string.IsNullOrEmpty(path)
            || string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException(nameof(path));
        }

        path = CheckIfPathAbsoluteOrMakeItOne(path);

        return Directory.Exists(path);
    }
}