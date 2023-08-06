namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.PathManager;

using System.Text;
using Microsoft.Extensions.Logging;

public partial class PathManagerImpl
{
    /// <inheritdoc />
    public string CheckIfPathAbsoluteOrMakeItOne(string path)
    {
        if (string.IsNullOrEmpty(path)
            || string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException(nameof(path));
        }

        if (path[0].ToString() == "/")
        {
            return path;
        }

        StringBuilder builder = new StringBuilder();
        builder
            .Append(GetCurrentDirectory())
            .Append("/")
            .Append(path);

        _logger.LogInformation(
            "Path is not absolute path. Will make it one" +
            "Old path: {OldPath}, new path: {NewPath}",
            path, builder.ToString());

        return builder.ToString();
    }
}