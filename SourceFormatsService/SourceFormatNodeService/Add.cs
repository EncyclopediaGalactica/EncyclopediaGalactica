namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.SourceFormatNodeService;

using System.Runtime.CompilerServices;
using Dtos;
using Entities;
using FluentValidation;
using Guards;
using ValidatorService;

public partial class SourceFormatNodeService
{
    public async Task<SourceFormatNodeDto> AddAsync(
        SourceFormatNodeDto dto,
        CancellationToken cancellationToken = default)
    {
        Guard.NotNull(dto);
        await ValidateInputAsync(dto).ConfigureAwait(false);
        SourceFormatNode sourceFormatNode = MapSourceFormatNodeDtoToSourceFormatNode(dto);
        SourceFormatNode result = await PersistSourceFormatNodeAsync(sourceFormatNode, cancellationToken);
        await AppendToSourceFormatNodesCachedList(result, SourceFormatNodesListKey);
        SourceFormatNodeDto mappedResult = MapSourceFormatToSourceFormatNodeDto(result);
        return mappedResult;
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