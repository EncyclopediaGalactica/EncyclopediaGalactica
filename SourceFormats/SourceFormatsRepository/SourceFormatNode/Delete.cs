namespace EncyclopediaGalactica.SourceFormats.SourceFormatsRepository.SourceFormatNode;

using Entities;
using Exceptions;

public partial class SourceFormatNodeRepository
{
    /// <inheritdoc />
    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        try
        {
            _guards.NotNull(id);
            _guards.IsNotEqual(id, 0);

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