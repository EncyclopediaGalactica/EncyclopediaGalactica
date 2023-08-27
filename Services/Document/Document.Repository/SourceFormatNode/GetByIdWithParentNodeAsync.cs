namespace EncyclopediaGalactica.Services.Document.SourceFormatsRepository.SourceFormatNode;

using Ctx;
using Entities;
using Microsoft.EntityFrameworkCore;

public partial class SourceFormatNodeRepository
{
    /// <inheritdoc />
    public async Task<SourceFormatNode> GetByIdWithRootNodeAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _guards.IsNotEqual(id, 0);

            await using (DocumentDbContext ctx = new DocumentDbContext(_dbContextOptions))
            {
                SourceFormatNode result = await ctx.SourceFormatNodes
                    .Include(i => i.RootNode)
                    .FirstAsync(w => w.Id == id, cancellationToken)
                    .ConfigureAwait(false);
                return result;
            }
        }
        catch (Exception e)
        {
            // logging comes here
            throw;
        }
    }
}