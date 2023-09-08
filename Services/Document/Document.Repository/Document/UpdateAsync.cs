namespace EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Document;

using Ctx;
using Entities;
using Exceptions;
using Microsoft.EntityFrameworkCore;

public partial class DocumentRepository
{
    /// <inheritdoc />
    public async Task<Document> UpdateAsync(
        long documentId,
        Document documentWithNewValues,
        CancellationToken cancellationToken = default)
    {
        await using (DocumentDbContext ctx = new DocumentDbContext(_dbContextOptions))
        {
            Document? toBeModified = await ctx.Documents.FindAsync(documentWithNewValues.Id);
            CheckIfDocumentBeenFoundOrThrow(toBeModified!, documentId);
#pragma warning disable CS8604 // Possible null reference argument.
            ModifyDocumentValues(toBeModified, documentWithNewValues);
#pragma warning restore CS8604 // Possible null reference argument.
            ctx.Entry(toBeModified).State = EntityState.Modified;
            await ctx.SaveChangesAsync(cancellationToken);
            return toBeModified;
        }
    }

    private void ModifyDocumentValues(Document toBeModified, Document documentWithNewValues)
    {
        // The input is already validated, so there is no reason to validate them again
        if (toBeModified.Name != documentWithNewValues.Name)
        {
            toBeModified.Name = documentWithNewValues.Name;
        }

        if (toBeModified.Description != documentWithNewValues.Description)
        {
            toBeModified.Description = documentWithNewValues.Description;
        }

        if (toBeModified.Uri != documentWithNewValues.Uri)
        {
            toBeModified.Uri = documentWithNewValues.Uri;
        }
    }

    private void CheckIfDocumentBeenFoundOrThrow(Document toBeModified, long documentId)
    {
        if (toBeModified is null)
        {
            string msg = $"There is no document with {documentId}";
            throw new DocumentNotFoundException(msg);
        }
    }
}