namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.SourceFormatNode;

using Entities;
using Exceptions;
using Guards;
using Microsoft.EntityFrameworkCore;

public partial class SourceFormatNodeRepository
{
    /// <inheritdoc />
    public async Task<List<SourceFormatNode>> GetByIdWithFlatTreeAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            Guard.IsNotEqual(id, 0);
            _ctx.ChangeTracker.Clear();
            SourceFormatNode? startNodeInTree = await _ctx.SourceFormatNodes
                .FindAsync(id).ConfigureAwait(false);
            if (startNodeInTree is null)
                throw new SourceFormatNodeRepositoryException(
                    $"No {nameof(SourceFormatNode)} entity in the system with id: {id}");
            if (startNodeInTree.ChildrenSourceFormatNodes.Any())
                throw new SourceFormatNodeRepositoryException(
                    $"Entity with id: {id} should not include its children.");

            List<SourceFormatNode> sourceFormatNodes = await _ctx.SourceFormatNodes
                .Where(w => w.RootNodeId == startNodeInTree.RootNodeId)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            List<SourceFormatNode> result = GetFlatTree(startNodeInTree, sourceFormatNodes);
            return result;
        }
        catch (Exception e)
        {
            string msg = prepErrorMessage(nameof(GetByIdWithFlatTreeAsync));
            throw new SourceFormatNodeRepositoryException(msg, e);
        }
    }

    private List<SourceFormatNode> GetFlatTree(SourceFormatNode node, List<SourceFormatNode> sourceFormatNodes)
    {
        List<SourceFormatNode> result = new List<SourceFormatNode>();

        if (node.ChildrenSourceFormatNodes.Any())
        {
            foreach (SourceFormatNode child in node.ChildrenSourceFormatNodes)
            {
                result.AddRange(GetFlatTree(child, sourceFormatNodes));
            }
        }

        result.Add(node);

        return result;
    }
}