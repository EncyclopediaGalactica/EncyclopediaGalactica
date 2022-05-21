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
    [Route("get")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SourceFormatNodeGetAllResponseModel>> GetAsync()
    {
        SourceFormatNodeListResultResponseModel result = await _sourceFormatsService
            .SourceFormatNode
            .GetAllAsync()
            .ConfigureAwait(false);

        _logger.LogInformation("{MethodName} executed", nameof(GetAsync));

        switch (result.Status)
        {
            case SourceFormatsServiceResultStatuses.Success:
                SourceFormatNodeListResultViewModel successViewModel = new()
                {
                    Result = result.Result,
                    IsOperationSuccessful = result.IsOperationSuccessful,
                    Message = SourceFormatsServiceResultStatuses.Success
                };
                return Ok(successViewModel);

            case SourceFormatsServiceResultStatuses.ValidationError:
                SourceFormatNodeListResultViewModel validationErrorViewModel = new()
                {
                    Result = null,
                    IsOperationSuccessful = result.IsOperationSuccessful,
                    Message = SourceFormatsServiceResultStatuses.ValidationError
                };
                return BadRequest(validationErrorViewModel);

            case SourceFormatsServiceResultStatuses.InternalError:
                return Problem(null, "Internal Server error", (int)HttpStatusCode.InternalServerError);

            default:
                return Problem("Something strange");
        }
    }
}