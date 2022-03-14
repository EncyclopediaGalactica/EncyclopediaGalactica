namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.SourceFormatNode;

using Entities;
using Exceptions;
using Guards;

public partial class SourceFormatNodeRepository
{
    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        try
        {
            Guard.NotNull(id);
            Guard.IsNotEqual(id, 0);

            List<SourceFormatNode> toBeDelete = await GetByIdWithFlatTreeAsync(id, cancellationToken)
                .ConfigureAwait(false);

            if (toBeDelete.Any())
            {
                _ctx.SourceFormatNodes.RemoveRange(toBeDelete);
                await _ctx.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                return;
            }

            throw new SourceFormatNodeRepositoryException($"No {nameof(SourceFormatNode)} entity with id:{id}.");
        }
        catch (Exception e)
        {
            string msg = prepErrorMessage(nameof(DeleteAsync));
            throw new SourceFormatNodeRepositoryException(msg, e);
        }
    }
}