namespace EncyclopediaGalactica.Services.Document.Repository.SourceFormatNode;

using Ctx;
using Entities;
using Microsoft.EntityFrameworkCore;

public partial class SourceFormatNodeRepository
{
    /// <inheritdoc />
    public async Task<List<SourceFormatNode>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        await using DocumentDbContext ctx = new DocumentDbContext(_dbContextOptions);
        try
        {
            return await ctx.SourceFormatNodes.ToListAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            // logging comes here
            throw;
        }
    }
}