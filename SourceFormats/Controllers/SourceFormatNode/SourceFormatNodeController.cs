namespace Controllers.SourceFormatNode;

using EncyclopediaGalactica.SourceFormats.Api;
using EncyclopediaGalactica.SourceFormats.SourceFormatsService.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route(SourceFormatNode.Route)]
public partial class SourceFormatNodeController : ControllerBase
{
    private readonly ISourceFormatsService _sourceFormatsService;

    public SourceFormatNodeController(ISourceFormatsService sourceFormatsService)
    {
        _sourceFormatsService = sourceFormatsService;
    }
}