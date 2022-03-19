namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Interfaces;

using Dtos;
using Entities;

public interface ISourceFormatNodeService
{
    Task<SourceFormatNodeDto> AddAsync(
        SourceFormatNodeDto dto,
        CancellationToken cancellationToken = default);

    Task<SourceFormatNodeDto> AddSourceFormatNodeChildToParent(
        SourceFormatNodeDto childDto,
        SourceFormatNodeDto parentDto,
        CancellationToken cancellationToken);

    Task<SourceFormatNode> GetSourceFormatNodeByIdAsync(long id, CancellationToken cancellationToken = default);

    Task<SourceFormatNodeDto> GetSourceFormatNodeByIdWithChildrenAsync(
        long id,
        CancellationToken cancellationToken = default);

    Task<SourceFormatNodeDto> GetSourceFormatNodeByIdWithNodeTreeAsync(
        long id,
        CancellationToken cancellationToken = default);

    Task<ICollection<SourceFormatNode>> GetSourceFormatNodesAsync(CancellationToken cancellationToken = default);

    Task<SourceFormatNodeDto> UpdateSourceFormatNodeAsync(
        SourceFormatNodeDto dto,
        CancellationToken cancellationToken = default);

    Task DeleteSourceFormatNodeAsync(SourceFormatNodeDto dto, CancellationToken cancellationToken = default);
}