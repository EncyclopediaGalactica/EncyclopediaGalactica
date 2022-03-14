namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.SourceFormatNode;

using Entities;
using Exceptions;
using Microsoft.EntityFrameworkCore;

public partial class SourceFormatNodeRepository
{
    public async Task<SourceFormatNode> GetByIdWithChildrenAsync(long id, CancellationToken cancellationToken = default)
    {
        try
        {
            if (id == 0)
                throw new SourceFormatNodeRepositoryException("Id cannot be zero.");

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