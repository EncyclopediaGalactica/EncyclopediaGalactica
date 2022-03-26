namespace EncyclopediaGalactica.SourceFormats.Controllers.SourceFormatNode;

using Api;
using Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public partial class SourceFormatNodeController
{
    [HttpPost]
    [Route(SourceFormats.Route)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SourceFormatNodeDto>> AddAsync(SourceFormatNodeDto dto)
    {
        SourceFormatNodeDto result = await _sourceFormatsService
            .SourceFormatNodeService
            .AddAsync(dto)
            .ConfigureAwait(false);
        return Ok(result);
    }
}