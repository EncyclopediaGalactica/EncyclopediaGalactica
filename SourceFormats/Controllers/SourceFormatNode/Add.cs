namespace EncyclopediaGalactica.SourceFormats.Controllers.SourceFormatNode;

using Api;
using Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public partial class SourceFormatNodeController
{
    [HttpPost(SourceFormatNode.Add)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SourceFormatNodeDto>> AddAsync([FromBody] SourceFormatNodeDto dto)
    {
        SourceFormatNodeDto result = await _sourceFormatsService
            .SourceFormatNode
            .AddAsync(dto)
            .ConfigureAwait(false);
        return Ok(result);
    }
}