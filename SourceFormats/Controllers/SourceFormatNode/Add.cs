namespace EncyclopediaGalactica.SourceFormats.Controllers.SourceFormatNode;

using Api;
using Dtos;
using Microsoft.AspNetCore.Mvc;
using SourceFormatsService.Exceptions;

public partial class SourceFormatNodeController
{
    [HttpPost]
    [Route(SourceFormats.Route)]
    public async Task<ActionResult<SourceFormatNodeDto>> AddAsync(SourceFormatNodeDto dto)
    {
        try
        {
            SourceFormatNodeDto result = await _sourceFormatsService
                .SourceFormatNodeService
                .AddAsync(dto)
                .ConfigureAwait(false);
            return Ok(result);
        }
        catch (Exception e) when (e is SourceFormatNodeServiceInputValidationException)
        {
            return BadRequest(StatusCode(400));
        }
        catch (Exception ex) when (
            ex is SourceFormatNodeServiceException)
        {
            return Problem(statusCode: 500);
        }
    }
}