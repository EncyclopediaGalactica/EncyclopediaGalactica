namespace EncyclopediaGalactica.SourceFormats.SourceFormatsRepository.SourceFormatNode;

using Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Exceptions;

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