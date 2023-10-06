namespace EncyclopediaGalactica.Services.Document.Repository.SourceFormatNode;

using Ctx;
using Entities;

public partial class SourceFormatNodeRepository
{
    /// <inheritdoc />
    public async Task<SourceFormatNode> GetByIdAsync(long id)
    {
        await using DocumentDbContext ctx = new DocumentDbContext(_dbContextOptions);
        try
        {
            _guards.IsNotEqual(id, 0);

            SourceFormatNode? result = await ctx.SourceFormatNodes.FindAsync(id).ConfigureAwait(false);

            if (result is null)
                throw new InvalidOperationException(
                    $"No {nameof(SourceFormatNode)} entity with id: {id}");

            return result;
        }
        catch (Exception e)
        {
            // logging comes here
            throw;
        }
    }
}