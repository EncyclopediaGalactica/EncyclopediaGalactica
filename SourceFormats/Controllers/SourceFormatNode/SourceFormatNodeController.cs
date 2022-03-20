namespace EncyclopediaGalactica.SourceFormats.Controllers.SourceFormatNode;

using Api;
using Microsoft.AspNetCore.Mvc;
using SourceFormatsService.Interfaces;

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