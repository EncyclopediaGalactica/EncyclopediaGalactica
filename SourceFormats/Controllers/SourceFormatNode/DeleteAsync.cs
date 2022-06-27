namespace EncyclopediaGalactica.SourceFormats.Controllers.SourceFormatNode;

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
    [HttpDelete]
    [Route("delete")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(SourceFormatNodeSingleResultResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SourceFormatNodeSingleResultViewModel>> DeleteAsync(
        [FromBody] SourceFormatNodeDto? dto)
    {
        if (dto is null)
            _logger.LogInformation("{RequestModel} is null", nameof(dto));

        SourceFormatNodeSingleResultResponseModel result = await _sourceFormatsService
            .SourceFormatNode
            .DeleteAsync(dto!)
            .ConfigureAwait(false);

        switch (result.Status)
        {
            case SourceFormatsServiceResultStatuses.Success:
                SourceFormatNodeSingleResultViewModel successViewModel = new()
                {
                    Result = null,
                    IsOperationSuccessful = true,
                    Message = SourceFormatsServiceResultStatuses.Success
                };
                return successViewModel;

            case SourceFormatsServiceResultStatuses.ValidationError:
                SourceFormatNodeSingleResultViewModel validationErrorViewModel = new()
                {
                    Result = null,
                    IsOperationSuccessful = false,
                    Message = SourceFormatsServiceResultStatuses.ValidationError
                };
                return validationErrorViewModel;

            case SourceFormatsServiceResultStatuses.NoSuchEntity:
                SourceFormatNodeSingleResultViewModel noSuchEntityViewModel = new()
                {
                    Result = null,
                    IsOperationSuccessful = false,
                    Message = SourceFormatsServiceResultStatuses.NoSuchEntity
                };
                return noSuchEntityViewModel;

            case SourceFormatsServiceResultStatuses.InternalError:
                SourceFormatNodeSingleResultViewModel internalErrorViewModel = new()
                {
                    Result = null,
                    IsOperationSuccessful = false,
                    Message = SourceFormatsServiceResultStatuses.InternalError
                };
                return internalErrorViewModel;

            default:
                return Problem("Something is really wrong");
        }
    }
}