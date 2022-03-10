namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.SourceFormatNode;

using Entities;
using Exceptions;
using Guards;
using Microsoft.EntityFrameworkCore;

public partial class SourceFormatNodeRepository
{
    public async Task<SourceFormatNode> AddChildNodeAsync(
        long childId,
        long parentId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            Guard.IsNotEqual(childId, 0);
            Guard.IsNotEqual(parentId, 0);
            Guard.IsNotEqual(childId, parentId);

            await _ctx.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
            SourceFormatNode? child = await _ctx.SourceFormatNodes.FindAsync(childId).ConfigureAwait(false);
            SourceFormatNode? parent = await _ctx.SourceFormatNodes.FindAsync(parentId).ConfigureAwait(false);

            if (child is null)
                throw new SourceFormatNodeRepositoryException(
                    $"No {nameof(SourceFormatNode)} entity with id: {childId}");
            if (parent is null)
                throw new SourceFormatNodeRepositoryException(
                    $"No {nameof(SourceFormatNode)} entity with id: {parentId}");

            child.ParentNodeId = parent.Id;
            _ctx.Entry(child).State = EntityState.Modified;
            await _ctx.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            await _ctx.Database.CommitTransactionAsync(cancellationToken).ConfigureAwait(false);
            return child;
        }
        catch (Exception e)
        {
            await _ctx.Database.RollbackTransactionAsync(cancellationToken).ConfigureAwait(false);
            string msg = $"Error happened while executing {nameof(SourceFormatNodeRepository)}." +
                         $"{nameof(AddChildNodeAsync)}. For further information see inner exception.";
            throw new SourceFormatNodeRepositoryException(msg, e);
        }
    }
}