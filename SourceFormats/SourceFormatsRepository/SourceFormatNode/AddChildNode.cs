namespace EncyclopediaGalactica.SourceFormats.SourceFormatsRepository.SourceFormatNode;

using Ctx;
using Entities;
using Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

public partial class SourceFormatNodeRepository
{
    /// <inheritdoc />
    public async Task<SourceFormatNode> AddChildNodeAsync(
        long childId,
        long parentId,
        long rootNodeId,
        CancellationToken cancellationToken = default)
    {
        await using SourceFormatsDbContext ctx = new SourceFormatsDbContext(_dbContextOptions);
        await using (IDbContextTransaction transaction = await ctx.Database
                         .BeginTransactionAsync(cancellationToken).ConfigureAwait(false))
            try
            {
                CheckInputForAddChildNode(childId, parentId, rootNodeId);

                SourceFormatNode? child = await ctx.SourceFormatNodes
                    .FirstAsync(p => p.Id == childId, cancellationToken)
                    .ConfigureAwait(false);
                SourceFormatNode? parent = await ctx.SourceFormatNodes
                    .Include(i => i.ChildrenSourceFormatNodes)
                    .FirstAsync(p => p.Id == parentId, cancellationToken)
                    .ConfigureAwait(false);
                SourceFormatNode? rootNode = null;
                if (rootNodeId != parentId)
                {
                    rootNode = await ctx.SourceFormatNodes
                        .FirstAsync(r => r.RootNodeId == rootNodeId, cancellationToken)
                        .ConfigureAwait(false);
                }

                ValidateEntitiesStatesForAddChildNode(childId, parentId, rootNodeId, child, parent, rootNode);

                child.ParentNodeId = parent.Id;
                child.RootNodeId = rootNodeId;
                ctx.Entry(child).State = EntityState.Modified;
                await ctx.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
                return child;
            }
            catch (Exception e)
            {
                // add logging here
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                throw;
            }
    }

    private static void ValidateEntitiesStatesForAddChildNode(
        long childId,
        long parentId,
        long rootNodeId,
        SourceFormatNode? child,
        SourceFormatNode? parent,
        SourceFormatNode? rootNode)
    {
        if (child is null)
            throw new SourceFormatNodeRepositoryException(
                $"No {nameof(SourceFormatNode)} entity with id: {childId}");

        if (parent is null)
            throw new SourceFormatNodeRepositoryException(
                $"No {nameof(SourceFormatNode)} entity with id: {parentId}");

        // Root node and parent node can be the same.
        // When they are the same validation is covered when we check parent null or not
        // When they are different we check whether root node exist in the db or not
        if (rootNodeId != parentId && rootNode is null)
            throw new SourceFormatNodeRepositoryException(
                $"Root {nameof(SourceFormatNode)} with id: {rootNodeId} does not exist.");

        if (parent.ChildrenSourceFormatNodes.ToList().FindIndex(i => i.Id == childId) != -1)
            throw new SourceFormatNodeRepositoryException(
                $"Entity with id {child} is already added to entity with id {parentId}");
    }

    private void CheckInputForAddChildNode(long childId, long parentId, long rootNodeId)
    {
        _guards.IsNotEqual(childId, 0);
        _guards.IsNotEqual(parentId, 0);
        _guards.IsNotEqual(rootNodeId, 0);
        _guards.IsNotEqual(childId, parentId);
        _guards.IsNotEqual(childId, rootNodeId);
    }
}