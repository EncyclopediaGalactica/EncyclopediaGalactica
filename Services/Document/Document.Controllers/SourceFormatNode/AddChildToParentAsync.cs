namespace EncyclopediaGalactica.Services.Document.Controllers.SourceFormatNode;

using System.Net.Mime;
using Contracts.Input;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public async Task<ActionResult<SourceFormatNodeInputContract>> AddChildToParentAsync(
        [FromBody] SourceFormatNodeInputContract? dto)
    {
        if (dto is null)
            _logger.LogInformation("{RequestModel} is null", nameof(dto));

        return await _sourceFormatsService
            .SourceFormatNode
            .AddChildToParentAsync(
                new SourceFormatNodeInputContract { Id = dto!.Id },
                new SourceFormatNodeInputContract { Id = (long)dto.ParentNodeId! })
            .ConfigureAwait(false);
    }
}