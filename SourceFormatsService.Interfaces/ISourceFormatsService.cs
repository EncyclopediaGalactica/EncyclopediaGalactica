namespace SourceFormatsService.Interfaces;

using EncyclopediaGalactica.SourceFormats.Worker.Entities;
using SourceFormatNodeDtos;

public interface ISourceFormatsService
{
    Task<SourceFormatNode> AddSourceFormatNodeAsync(
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