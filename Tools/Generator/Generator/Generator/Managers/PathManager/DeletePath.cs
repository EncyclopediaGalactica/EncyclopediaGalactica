namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.PathManager;

public partial class PathManagerImpl
{
    /// <inheritdoc />
    public void DeletePath(string path, bool recursive = false)
    {
        if (string.IsNullOrEmpty(path)
            || string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException(nameof(path));
        }

        path = CheckIfPathAbsoluteOrMakeItOne(path);

        Directory.Delete(path, recursive);
    }
}