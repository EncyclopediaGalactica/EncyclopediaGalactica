namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.Interfaces;

using Entities;

public interface ISourceFormatsNodeRepository
{
    Task<SourceFormatNode> AddAsync(SourceFormatNode node, CancellationToken cancellationToken = default);

    Task<SourceFormatNode> AddChildNodeAsync(
        long childId,
        long parentId,
        long rootNodeId,
        CancellationToken cancellationToken = default);

    Task<SourceFormatNode> UpdateAsync(SourceFormatNode node, CancellationToken cancellationToken = default);

    Task DeleteAsync(long id, CancellationToken cancellationToken = default);

    Task<List<SourceFormatNode>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<SourceFormatNode> GetByIdWithChildrenAsync(long id, CancellationToken cancellationToken = default);

    Task<SourceFormatNode> GetByIdAsync(long id);

    Task<List<SourceFormatNode>> GetByIdWithFlatTreeAsync(long id, CancellationToken cancellationToken = default);
}