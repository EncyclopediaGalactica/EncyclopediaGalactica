namespace Controllers.SourceFormatNode;

using EncyclopediaGalactica.SourceFormats.Api;
using EncyclopediaGalactica.SourceFormats.Dtos;
using EncyclopediaGalactica.SourceFormats.Mappers.Exceptions.SourceFormatNode;
using EncyclopediaGalactica.SourceFormats.Repository.Exceptions;
using FluentValidation;
using Guards;
using Microsoft.AspNetCore.Mvc;
using SourceFormatsCacheService.Exceptions;

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
        catch (Exception e) when (e is GuardException || e is ValidationException)
        {
            return BadRequest(StatusCode(400));
        }
        catch (Exception ex) when (
            ex is SourceFormatNodeMapperException
            || ex is SourceFormatNodeRepositoryException
            || ex is SourceFormatsCacheServiceException)
        {
            return Problem(statusCode: 500);
        }
    }
}