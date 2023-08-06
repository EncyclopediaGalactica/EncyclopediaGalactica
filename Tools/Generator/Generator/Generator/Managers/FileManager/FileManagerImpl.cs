namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.FileManager;

using Microsoft.Extensions.Logging;

/// <inheritdoc />
public partial class FileManagerImpl : IFileManager
{
    private readonly ILogger<FileManagerImpl> _logger =
        new Logger<FileManagerImpl>(LoggerFactory.Create(c => { c.AddConsole(); }));
}