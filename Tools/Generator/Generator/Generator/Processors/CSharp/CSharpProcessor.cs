namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Processors.CSharp;

using Managers.FileManager;
using Managers.PathManager;
using Managers.StringManager;
using Microsoft.Extensions.Logging;
using Structure;

public partial class CSharpProcessor : ProcessorAbstract, ICSharpProcessor
{
    private readonly IFileManager _fileManager;

    private readonly ILogger<CSharpProcessor> _logger = new Logger<CSharpProcessor>(
        LoggerFactory.Create(o => o.AddConsole()));

    private readonly IPathManager _pathManager;
    private readonly IStringManager _stringManager;
    private readonly IStructureDescriptor _structureDescriptor;

    public CSharpProcessor(
        IFileManager fileManager,
        IStringManager stringManager,
        IPathManager pathManager,
        IStructureDescriptor structureDescriptor)
    {
        ArgumentNullException.ThrowIfNull(fileManager);
        ArgumentNullException.ThrowIfNull(stringManager);
        ArgumentNullException.ThrowIfNull(pathManager);
        ArgumentNullException.ThrowIfNull(structureDescriptor);

        _fileManager = fileManager;
        _stringManager = stringManager;
        _pathManager = pathManager;
        _structureDescriptor = structureDescriptor;
    }
}