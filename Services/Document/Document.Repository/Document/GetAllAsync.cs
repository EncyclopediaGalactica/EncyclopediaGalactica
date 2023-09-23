namespace EncyclopediaGalactica.Services.Document.Repository.Document;

using Ctx;
using Entities;
using Microsoft.EntityFrameworkCore;

public partial class DocumentRepository
{
    /// <inheritdoc />
    public async Task<List<Document>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        await using DocumentDbContext ctx = new DocumentDbContext(_dbContextOptions);
        return await ctx.Documents.ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}