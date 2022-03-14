namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.SourceFormatNode;

using Entities;
using Exceptions;

public partial class SourceFormatNodeRepository
{
    /// <inheritdoc />
    public async Task<SourceFormatNode> GetByIdAsync(long id)
    {
        try
        {
            if (id == 0)
                throw new SourceFormatNodeRepositoryException("Id cannot be 0.");

            SourceFormatNode? result = await _ctx.SourceFormatNodes.FindAsync(id).ConfigureAwait(false);

            if (result is null)
                throw new SourceFormatNodeRepositoryException(
                    $"No {nameof(SourceFormatNode)} entity with id: {id}");

            return result;
        }
        catch (Exception e)
        {
            string msg = prepErrorMessage(nameof(GetByIdAsync));
            throw new SourceFormatNodeRepositoryException(msg, e);
        }
    }
}