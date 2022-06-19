namespace EncyclopediaGalactica.SourceFormats.SourceFormatsRepository.SourceFormatNode;

using Ctx;
using Entities;
using Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

public partial class SourceFormatNodeRepository
{
    /// <inheritdoc />
    public async Task<List<SourceFormatNode>> GetByIdWithFlatTreeAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        await using SourceFormatsDbContext ctx = new SourceFormatsDbContext(_dbContextOptions);
        await using (IDbContextTransaction transaction = await ctx.Database
                         .BeginTransactionAsync(cancellationToken).ConfigureAwait(false))
        {
            try
            {
                _guards.IsNotEqual(id, 0);
                ctx.ChangeTracker.Clear();
                SourceFormatNode? startNodeInTree = await ctx.SourceFormatNodes
                    .FirstAsync(p => p.Id == id, cancellationToken)
                    .ConfigureAwait(false);
                if (startNodeInTree is null)
                    throw new SourceFormatNodeRepositoryException(
                        $"No {nameof(SourceFormatNode)} entity in the system with id: {id}");
                if (startNodeInTree.ChildrenSourceFormatNodes.Any())
                    throw new SourceFormatNodeRepositoryException(
                        $"Entity with id: {id} should not include its children.");

                List<SourceFormatNode> sourceFormatNodes = await ctx.SourceFormatNodes
                    .Where(w => w.RootNodeId == startNodeInTree.RootNodeId)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);

                List<SourceFormatNode> result = GetFlatTree(startNodeInTree, sourceFormatNodes);
                await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
                return result;
            }
            catch (Exception e)
            {
                // logging comes here
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                throw;
            }
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