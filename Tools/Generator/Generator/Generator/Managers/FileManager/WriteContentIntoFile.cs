namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.FileManager;

using System.Text;
using Microsoft.Extensions.Logging;

public partial class FileManagerImpl
{
    /// <inheritdoc />
    public void WriteContentIntoFile(string content, string pathToFile)
    {
        ASCIIEncoding asciiEncoding = new ASCIIEncoding();
        using FileStream fileStream = File.Open(pathToFile,
            FileMode.OpenOrCreate,
            FileAccess.ReadWrite,
            FileShare.Inheritable);
        fileStream.Write(asciiEncoding.GetBytes(content), 0, asciiEncoding.GetByteCount(content));
        _logger.LogInformation("Content is written in the {file}", pathToFile);
    }
}