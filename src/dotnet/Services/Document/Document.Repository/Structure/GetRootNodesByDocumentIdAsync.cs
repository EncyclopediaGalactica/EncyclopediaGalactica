namespace EncyclopediaGalactica.Services.Document.Repository.Structure;

using Ctx;
using Entities;
using Microsoft.EntityFrameworkCore;

public partial class StructureNodeRepository
{
    /// <inheritdoc />
    public async Task<List<StructureNode>> GetRootNodesByDocumentIdAsync(long documentId)
    {
        await using DocumentDbContext ctx = new DocumentDbContext(_dbContextOptions);
        return await ctx.StructureNodes
            .Where(w => w.IsRootNode == 1)
            .Where(w => w.DocumentId == documentId)
            .ToListAsync();
    }
}