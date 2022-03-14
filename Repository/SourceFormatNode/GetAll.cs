namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.SourceFormatNode;

using Entities;
using Exceptions;
using Microsoft.EntityFrameworkCore;

public partial class SourceFormatNodeRepository
{
    /// <inheritdoc />
    public async Task<List<SourceFormatNode>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _ctx.SourceFormatNodes.ToListAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            string msg = prepErrorMessage(nameof(GetAllAsync));
            throw new SourceFormatNodeRepositoryException(msg, e);
        }
    }
}