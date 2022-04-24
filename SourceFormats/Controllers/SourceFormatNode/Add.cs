namespace EncyclopediaGalactica.SourceFormats.Controllers.SourceFormatNode;

using System.Net;
using System.Net.Mime;
using Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sdk.Models.SourceFormatNode;

public partial class SourceFormatNodeController
{
    [HttpPost]
    [Route("add")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(SourceFormatNodeAddResponseModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SourceFormatNodeAddResponseModel>> AddAsync(
        [FromBody] SourceFormatNodeDto dto)
    {
        if (dto is null)
            _logger.LogInformation("{RequestModel} is null", nameof(dto));

        SourceFormatNodeAddResponseModel result = await _sourceFormatsService
            .SourceFormatNode
            .AddAsync(dto)
            .ConfigureAwait(false);

        switch (result.HttpStatusCode)
        {
            case (int)HttpStatusCode.Created:
                return Created(new Uri($"http://localhost/{result.Result.Id}"), result);
                break;

            case (int)HttpStatusCode.BadRequest:
                return BadRequest(result);
                break;

            case (int)HttpStatusCode.InternalServerError:
                return Problem(null, "Internal Server error", (int)HttpStatusCode.InternalServerError, null);
                break;

            default:
                return Problem("Something strange");
        }
    }
}