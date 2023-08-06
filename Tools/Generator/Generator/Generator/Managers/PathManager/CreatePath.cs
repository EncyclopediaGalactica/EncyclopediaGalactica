namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.PathManager;

public partial class PathManagerImpl
{
    /// <inheritdoc />
    public void CreatePath(string path)
    {
        if (string.IsNullOrEmpty(path)
            || string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException(nameof(path));
        }

        path = CheckIfPathAbsoluteOrMakeItOne(path);

        Directory.CreateDirectory(path);
    }
}