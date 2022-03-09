namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.Interfaces;

using Entities;

public interface ISourceFormatsNodeRepository
{
    Task<SourceFormatNode> AddAsync(SourceFormatNode node, CancellationToken cancellationToken = default);
    Task<SourceFormatNode> AddChildNode(SourceFormatNode child, SourceFormatNode parent);
    SourceFormatNode Update(SourceFormatNode node);
    void Delete(SourceFormatNode node);
    ICollection<SourceFormatNode> GetAll();
    SourceFormatNode GetById(long id);
    Task<SourceFormatNode> GetNodeWithChildren(SourceFormatNode node);
    Task<SourceFormatNode> GetNodeWithTree(SourceFormatNode node);
}