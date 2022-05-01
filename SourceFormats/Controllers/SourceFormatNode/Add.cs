namespace EncyclopediaGalactica.SourceFormats.Controllers.SourceFormatNode;

using System.Net;
using System.Net.Mime;
using Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SourceFormatsService.Interfaces;
using SourceFormatsService.Interfaces.SourceFormatNode;
using ViewModels;

public partial class SourceFormatNodeController
{
    [HttpPost]
    [Route("add")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(SourceFormatNodeSingleResultViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SourceFormatNodeSingleResultViewModel>> AddAsync(
        [FromBody] SourceFormatNodeDto? dto)
    {
        if (dto is null)
            _logger.LogInformation("{RequestModel} is null", nameof(dto));

        SourceFormatNodeSingleResultResponseModel result = await _sourceFormatsService
            .SourceFormatNode
            .AddAsync(dto)
            .ConfigureAwait(false);

        switch (result.Status)
        {
            case SourceFormatsResultStatuses.Success:
                SourceFormatNodeSingleResultViewModel successViewModel = new()
                {
                    Result = result.Result,
                    IsOperationSuccessful = result.IsOperationSuccessful,
                    Message = SourceFormatsResultStatuses.Success
                };
                return Created(new Uri($"http://localhost/{successViewModel.Result.Id}"), successViewModel);

            case SourceFormatsResultStatuses.ValidationError:
                SourceFormatNodeSingleResultViewModel validationErrorViewModel = new()
                {
                    Result = null,
                    IsOperationSuccessful = result.IsOperationSuccessful,
                    Message = SourceFormatsResultStatuses.ValidationError
                };
                return BadRequest(validationErrorViewModel);

            case SourceFormatsResultStatuses.InternalError:
                return Problem(null, "Internal Server error", (int)HttpStatusCode.InternalServerError);

            default:
                return Problem("Something strange");
        }
    }
}