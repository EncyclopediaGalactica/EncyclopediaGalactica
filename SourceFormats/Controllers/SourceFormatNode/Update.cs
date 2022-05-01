namespace EncyclopediaGalactica.SourceFormats.Controllers.SourceFormatNode;

using System.Net;
using Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sdk.Models.SourceFormatNode;
using SourceFormatsService.Interfaces;
using SourceFormatsService.Interfaces.SourceFormatNode;
using ViewModels;

public partial class SourceFormatNodeController
{
    [HttpPut]
    [Route("update")]
    [ProducesResponseType(typeof(SourceFormatNodeUpdateResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SourceFormatNodeUpdateResponseModel>> Update(
        [FromBody] SourceFormatNodeDto? dto)
    {
        if (dto is null)
            _logger.LogWarning("{Dto} is null ", dto);

        SourceFormatNodeSingleResultResponseModel responseModel = await _sourceFormatsService
            .SourceFormatNode
            .UpdateSourceFormatNodeAsync(dto)
            .ConfigureAwait(false);

        switch (responseModel.Status)
        {
            case SourceFormatsResultStatuses.Success:
                SourceFormatNodeSingleResultViewModel successViewModel = new()
                {
                    IsOperationSuccessful = responseModel.IsOperationSuccessful,
                    Result = responseModel.Result,
                    Message = SourceFormatsResultStatuses.Success
                };
                return Ok(successViewModel);

            case SourceFormatsResultStatuses.ValidationError:
                SourceFormatNodeSingleResultViewModel validationErrorViewModel = new()
                {
                    IsOperationSuccessful = responseModel.IsOperationSuccessful,
                    Result = null,
                    Message = SourceFormatsResultStatuses.ValidationError
                };
                return BadRequest(responseModel);

            case SourceFormatsResultStatuses.NoSuchEntity:
                SourceFormatNodeSingleResultViewModel noSuchEntityViewModel = new()
                {
                    IsOperationSuccessful = responseModel.IsOperationSuccessful,
                    Result = null,
                    Message = SourceFormatsResultStatuses.NoSuchEntity
                };
                return NotFound(noSuchEntityViewModel);

            case SourceFormatsResultStatuses.InternalError:
                return Problem("Error", "Error happened", (int)HttpStatusCode.InternalServerError);

            default:
                return Problem("Default error", "Default error happened", (int)HttpStatusCode.InternalServerError);
        }
    }
}