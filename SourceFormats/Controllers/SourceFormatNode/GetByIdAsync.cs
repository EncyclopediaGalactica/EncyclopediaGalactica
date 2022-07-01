namespace EncyclopediaGalactica.SourceFormats.Controllers.SourceFormatNode;

using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sdk.Models.SourceFormatNode;
using SourceFormatsService.Interfaces;
using SourceFormatsService.Interfaces.SourceFormatNode;
using ViewModels;

public partial class SourceFormatNodeController
{
    [HttpGet]
    [Route("getbyid/{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SourceFormatNodeGetByIdResponseModel>> GetByIdAsync(
        long id)
    {
        SourceFormatNodeSingleResultResponseModel result = await _sourceFormatsService
            .SourceFormatNode
            .GetByIdAsync(id)
            .ConfigureAwait(false);

        _logger.LogInformation("{MethodName} is executed", nameof(GetByIdAsync));

        switch (result.Status)
        {
            case SourceFormatsServiceResultStatuses.Success:
                SourceFormatNodeSingleResultViewModel successViewModel = new SourceFormatNodeSingleResultViewModel
                {
                    IsOperationSuccessful = true,
                    Result = result.Result,
                    Message = SourceFormatsServiceResultStatuses.Success
                };
                return Ok(successViewModel);

            case SourceFormatsServiceResultStatuses.ValidationError:
                SourceFormatNodeSingleResultViewModel validationErrorViewModel =
                    new SourceFormatNodeSingleResultViewModel
                    {
                        IsOperationSuccessful = false,
                        Result = null,
                        Message = SourceFormatsServiceResultStatuses.ValidationError
                    };
                return BadRequest(validationErrorViewModel);

            case SourceFormatsServiceResultStatuses.NoSuchEntity:
                SourceFormatNodeSingleResultViewModel noSuchEntityViewModel = new SourceFormatNodeSingleResultViewModel
                {
                    IsOperationSuccessful = false,
                    Result = null,
                    Message = SourceFormatsServiceResultStatuses.NoSuchEntity
                };
                return NotFound(noSuchEntityViewModel);

            case SourceFormatsServiceResultStatuses.InternalError:
                return Problem(null, "Internal Server Error", (int)HttpStatusCode.InternalServerError);

            default:
                return Problem("Something is wrong");
        }
    }
}