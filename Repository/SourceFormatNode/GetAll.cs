namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.SourceFormatNode;

using Entities;
using Exceptions;
using Microsoft.EntityFrameworkCore;

public partial class SourceFormatNodeRepository
{
    public async Task<ICollection<SourceFormatNode>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _ctx.SourceFormatNodes.ToListAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            string msg =
                $"Error happened while executing {nameof(SourceFormatNodeRepository)}.{nameof(GetAllAsync)}. " +
                $"For further information see inner exception.";
            throw new SourceFormatNodeRepositoryException(msg, e);
        }
    }
}