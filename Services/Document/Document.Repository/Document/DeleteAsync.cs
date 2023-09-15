namespace EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Document;

using Ctx;
using Entities;
using Exceptions;
using Microsoft.EntityFrameworkCore;

public partial class DocumentRepository
{
    /// <inheritdoc />
    public async Task DeleteAsync(long documentId, CancellationToken cancellationToken = default)
    {
        await using (DocumentDbContext ctx = new DocumentDbContext(_dbContextOptions))
        {
            Document? toBeDeleted = await ctx.Documents.FindAsync(documentId, cancellationToken).ConfigureAwait(false);
            CheckIfDocumentToBeDeletedExistsOrThrow(documentId, toBeDeleted);
#pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
            ctx.Entry(toBeDeleted).State = EntityState.Deleted;
#pragma warning restore CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
            await ctx.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    private static void CheckIfDocumentToBeDeletedExistsOrThrow(long documentId, Document? toBeDeleted)
    {
        if (toBeDeleted is null)
        {
            string msg = $"{nameof(Document)} entity with id: {documentId} does not exist!";
            throw new DocumentNotFoundException(msg);
        }
    }
}