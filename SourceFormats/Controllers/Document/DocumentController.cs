namespace EncyclopediaGalactica.Services.Document.Controllers.Document;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SourceFormatsService.Interfaces;

[ApiController]
[Route("api/sourceformats/[controller]")]
public partial class DocumentController : ControllerBase
{
    private readonly ILogger<DocumentController> _logger;
    private readonly ISourceFormatsService _sourceFormatsService;

    public DocumentController(
        ISourceFormatsService sourceFormatsService,
        ILogger<DocumentController> logger)
    {
        ArgumentNullException.ThrowIfNull(sourceFormatsService);
        ArgumentNullException.ThrowIfNull(logger);

        _sourceFormatsService = sourceFormatsService;
        _logger = logger;
    }
}