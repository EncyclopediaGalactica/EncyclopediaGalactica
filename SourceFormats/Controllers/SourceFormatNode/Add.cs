namespace EncyclopediaGalactica.SourceFormats.Controllers.SourceFormatNode;

using Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sdk.Models.SourceFormatNode;

public partial class SourceFormatNodeController
{
    [HttpPost(SourceFormatNode.Add)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SourceFormatNodeAddResponseModel>> AddAsync(
        [FromBody] SourceFormatNodeAddRequestModel requestModel)
    {
        SourceFormatNodeAddResponseModel result = await _sourceFormatsService
            .SourceFormatNode
            .AddAsync(requestModel)
            .ConfigureAwait(false);
        return Ok(result);
    }
}