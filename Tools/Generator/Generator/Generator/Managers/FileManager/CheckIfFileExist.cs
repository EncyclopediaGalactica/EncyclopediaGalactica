namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.FileManager;

public partial class FileManagerImpl
{
    /// <inheritdoc />
    public bool CheckIfFileExist(string pathToFile)
    {
        if (!string.IsNullOrEmpty(pathToFile)
            && !string.IsNullOrWhiteSpace(pathToFile))
        {
            return File.Exists(pathToFile);
        }

        return false;
    }
}