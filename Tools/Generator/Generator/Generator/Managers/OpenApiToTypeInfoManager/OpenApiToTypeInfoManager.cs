namespace EncyclopediaGalactica.RestApiSdkGenerator.Generator.Generator.Managers.OpenApiToTypeInfoManager;

using Microsoft.Extensions.Logging;
using OpenApiDocumentManager;

/// <inheritdoc />
public partial class OpenApiToTypeInfoManager : IOpenApiToTypeInfoManager
{
    private readonly Logger<OpenApiToTypeInfoManager> _logger = new Logger<OpenApiToTypeInfoManager>(
        LoggerFactory.Create(c => { c.AddConsole(); }));

    private readonly IOpenApiDocumentManager _openApiDocumentManager;

    public OpenApiToTypeInfoManager()
    {
        _openApiDocumentManager = new Managers.OpenApiDocumentManager.OpenApiDocumentManager();
    }
}