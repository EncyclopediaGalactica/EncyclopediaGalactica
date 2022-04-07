namespace EncyclopediaGalactica.SourceFormats.Controllers.SourceFormatNode;

using Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sdk.Models.SourceFormatNode;

public partial class SourceFormatNodeController
{
    [HttpGet(SourceFormatNode.GetAll)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SourceFormatNodeGetAllResponseModel>> GetAsync()
    {
        SourceFormatNodeGetAllResponseModel result = await _sourceFormatsService
            .SourceFormatNode
            .GetAllAsync()
            .ConfigureAwait(false);
        return Ok(result);
    }
}