namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.Interfaces;

using Entities;

public interface ISourceFormatsNodeRepository
{
    Task<SourceFormatNode> AddAsync(SourceFormatNode node, CancellationToken cancellationToken = default);
    Task<SourceFormatNode> AddChildNode(SourceFormatNode child, SourceFormatNode parent);
    Task<SourceFormatNode> UpdateAsync(SourceFormatNode node, CancellationToken cancellationToken = default);
    void Delete(SourceFormatNode node);
    Task<ICollection<SourceFormatNode>> GetAll(CancellationToken cancellationToken = default);
    Task<SourceFormatNode> GetById(long id);
    Task<SourceFormatNode> GetNodeWithChildren(SourceFormatNode node);
    Task<SourceFormatNode> GetNodeWithTree(SourceFormatNode node);
}