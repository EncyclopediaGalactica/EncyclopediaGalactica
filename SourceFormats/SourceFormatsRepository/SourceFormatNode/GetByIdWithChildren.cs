namespace EncyclopediaGalactica.SourceFormats.SourceFormatsRepository.SourceFormatNode;

using Entities;
using Exceptions;
using Microsoft.EntityFrameworkCore;

public partial class SourceFormatNodeRepository
{
    /// <inheritdoc />
    public async Task<SourceFormatNode> GetByIdWithChildrenAsync(long id, CancellationToken cancellationToken = default)
    {
        try
        {
            _guards.IsNotEqual(id, 0);

            SourceFormatNode result = await _ctx.SourceFormatNodes
                .Include(i => i.ChildrenSourceFormatNodes)
                .FirstAsync(w => w.Id == id, cancellationToken)
                .ConfigureAwait(false);
            return result;
        }
        catch (Exception e)
        {
            string msg = prepErrorMessage(nameof(GetByIdWithChildrenAsync));
            throw new SourceFormatNodeRepositoryException(msg, e);
        }
    }
}