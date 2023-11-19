namespace EncyclopediaGalactica.Services.Document.Controllers.SourceFormatNode;

using System.Net.Mime;
using Contracts.Input;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public partial class SourceFormatNodeController
{
    [HttpGet]
    [Route("get")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<SourceFormatNodeInputContract>>> GetAsync()
    {
        return await _sourceFormatsService
            .SourceFormatNode
            .GetAllAsync()
            .ConfigureAwait(false);
    }
}