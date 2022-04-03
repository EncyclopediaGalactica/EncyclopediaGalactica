namespace EncyclopediaGalactica.SourceFormats.Controllers.SourceFormatNode;

using Api;
using Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public partial class SourceFormatNodeController
{
    [HttpGet(SourceFormatNode.GetAll)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<SourceFormatNodeDto>>> GetAsync()
    {
        List<SourceFormatNodeDto> result = await _sourceFormatsService
            .SourceFormatNode
            .GetAllAsync()
            .ConfigureAwait(false);
        return Ok(result);
    }
}