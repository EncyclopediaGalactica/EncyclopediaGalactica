using SourceFormatsCacheServiceException = SourceFormatsCacheService.Exceptions.SourceFormatsCacheServiceException;

namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.SourceFormatNodeService;

using System.Runtime.CompilerServices;
using Dtos;
using Entities;
using Exceptions;
using FluentValidation;
using Mappers.Exceptions.SourceFormatNode;
using Repository.Exceptions;
using ValidatorService;

public partial class SourceFormatNodeService
{
    public async Task<SourceFormatNodeDto> AddAsync(
        SourceFormatNodeDto dto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await ValidateInputAsync(dto).ConfigureAwait(false);
            SourceFormatNode sourceFormatNode = MapSourceFormatNodeDtoToSourceFormatNode(dto);
            SourceFormatNode result = await PersistSourceFormatNodeAsync(sourceFormatNode, cancellationToken);
            await AppendToSourceFormatNodesCachedList(result, SourceFormatNodesListKey);
            SourceFormatNodeDto mappedResult = MapSourceFormatToSourceFormatNodeDto(result);
            return mappedResult;
        }
        catch (Exception e) when (e is ArgumentNullException or ValidationException)
        {
            string msg = $"Input validation error at {nameof(SourceFormatNodeService)}.{nameof(AddAsync)}";
            throw new SourceFormatNodeServiceInputValidationException(msg, e);
        }
        catch (Exception e) when (e is SourceFormatNodeMapperException
                                      or SourceFormatNodeRepositoryException
                                      or SourceFormatsCacheServiceException)
        {
            string msg = $"Error happened while executing {nameof(SourceFormatNodeService)}.{nameof(AddAsync)}.";
            throw new SourceFormatNodeServiceException(msg, e);
        }
    }

    private SourceFormatNodeDto MapSourceFormatToSourceFormatNodeDto(SourceFormatNode node)
    {
        return _sourceFormatMappers
            .SourceFormatNodeMappers
            .MapSourceFormatNodeToSourceFormatNodeDto(node);
    }

    private async Task AppendToSourceFormatNodesCachedList(SourceFormatNode node, string key)
    {
        await _sourceFormatNodeCacheService.AppendToCache(node, key, _cacheExpiresInMinutes);
    }

    private ConfiguredTaskAwaitable<SourceFormatNode> PersistSourceFormatNodeAsync(
        SourceFormatNode sourceFormatNode,
        CancellationToken cancellationToken)
    {
        return _sourceFormatsNodeRepository.AddAsync(
                sourceFormatNode,
                cancellationToken)
            .ConfigureAwait(false);
    }

    private SourceFormatNode MapSourceFormatNodeDtoToSourceFormatNode(SourceFormatNodeDto dto)
    {
        return _sourceFormatMappers.SourceFormatNodeMappers.MapSourceFormatNodeDtoToSourceFormatNode(dto);
    }

    private async Task ValidateInputAsync(SourceFormatNodeDto dto)
    {
        await _sourceFormatNodeDtoValidator.ValidateAsync(dto, o =>
        {
            o.IncludeRuleSets(SourceFormatNodeDtoValidator.Add);
            o.ThrowOnFailures();
        }).ConfigureAwait(false);
    }
}