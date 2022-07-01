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
    [HttpPut]
    [Route("addchildtoparent")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(SourceFormatNodeSingleResultViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SourceFormatNodeSingleResultViewModel>> AddChildToParentAsync(
        [FromBody] SourceFormatNodeDto? dto)
    {
        if (dto is null)
            _logger.LogInformation("{RequestModel} is null", nameof(dto));

        SourceFormatNodeSingleResultResponseModel result = await _sourceFormatsService
            .SourceFormatNode
            .AddChildToParentAsync(
                new SourceFormatNodeDto { Id = dto!.Id },
                new SourceFormatNodeDto { Id = (long)dto.ParentNodeId! })
            .ConfigureAwait(false);

        switch (result.Status)
        {
            case SourceFormatsServiceResultStatuses.Success:
                SourceFormatNodeSingleResultViewModel successViewModel = new SourceFormatNodeSingleResultViewModel
                {
                    Result = result.Result,
                    IsOperationSuccessful = true,
                    Message = SourceFormatsServiceResultStatuses.Success
                };
                return Ok(successViewModel);

            case SourceFormatsServiceResultStatuses.ValidationError:
                SourceFormatNodeSingleResultViewModel validationErrorModel = new SourceFormatNodeSingleResultViewModel
                {
                    Result = null,
                    IsOperationSuccessful = false,
                    Message = SourceFormatsServiceResultStatuses.ValidationError
                };
                return BadRequest(validationErrorModel);

            case SourceFormatsServiceResultStatuses.NoSuchEntity:
                SourceFormatNodeSingleResultViewModel noSuchEntityModel = new SourceFormatNodeSingleResultViewModel
                {
                    Result = null,
                    IsOperationSuccessful = false,
                    Message = SourceFormatsServiceResultStatuses.NoSuchEntity
                };
                return NotFound(noSuchEntityModel);

            case SourceFormatsServiceResultStatuses.InternalError:
                return Problem(null, "Internal Server Error", (int)HttpStatusCode.InternalServerError);

            default:
                return Problem(null, "Something is wrong", (int)HttpStatusCode.InternalServerError);
        }
    }
}