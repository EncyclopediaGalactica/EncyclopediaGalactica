namespace EncyclopediaGalactica.Services.Document.Repository.Document;

using Ctx;
using Entities;
using Exceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ValidatorService;

public partial class DocumentRepository
{
    /// <inheritdoc />
    public async Task<Document> UpdateAsync(
        long documentId,
        Document documentWithNewValues,
        CancellationToken cancellationToken = default)
    {
        await ValidateUpdateAsyncInputAsync(documentWithNewValues, cancellationToken);
        await using (DocumentDbContext ctx = new DocumentDbContext(_dbContextOptions))
        {
            Document? toBeModified = await ctx.Documents.FindAsync(documentId, cancellationToken).ConfigureAwait(false);
            CheckIfDocumentBeenFoundOrThrow(toBeModified!, documentId);
#pragma warning disable CS8604 // Possible null reference argument.
            ModifyDocumentValues(toBeModified, documentWithNewValues);
#pragma warning restore CS8604 // Possible null reference argument.
            ctx.Entry(toBeModified).State = EntityState.Modified;
            await ctx.SaveChangesAsync(cancellationToken);
            return toBeModified;
        }
    }

    private async Task ValidateUpdateAsyncInputAsync(
        Document documentWithNewValues,
        CancellationToken cancellationToken)
    {
        await _documentValidator.ValidateAsync(
                documentWithNewValues,
                o =>
                {
                    o.ThrowOnFailures();
                    o.IncludeRuleSets(Operations.Update);
                },
                cancellationToken)
            .ConfigureAwait(false);
    }

    private void ModifyDocumentValues(Document toBeModified, Document documentWithNewValues)
    {
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