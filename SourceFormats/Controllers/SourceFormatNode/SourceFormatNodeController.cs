namespace EncyclopediaGalactica.SourceFormats.Controllers.SourceFormatNode;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SourceFormatsService.Interfaces;

[ApiController]
[Route("api/sourceformats/[controller]")]
public partial class SourceFormatNodeController : ControllerBase
{
    private readonly ISourceFormatsService _sourceFormatsService;
    private readonly ILogger<SourceFormatNodeController> _logger;

    public SourceFormatNodeController(
        ISourceFormatsService sourceFormatsService,
        ILogger<SourceFormatNodeController> logger)
    {
        ArgumentNullException.ThrowIfNull(sourceFormatsService);
        ArgumentNullException.ThrowIfNull(logger);

        _sourceFormatsService = sourceFormatsService;
        _logger = logger;
    }
}