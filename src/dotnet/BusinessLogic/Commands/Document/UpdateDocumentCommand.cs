namespace EncyclopediaGalactica.BusinessLogic.Commands.Document;

using Contracts;
using Database;
using Entities;
using Exceptions;
using FluentValidation;
using Mappers;
using Microsoft.EntityFrameworkCore;
using Validators;

public class UpdateDocumentCommand(
    IDocumentMapper documentMapper,
    IValidator<DocumentInput> documentInputValidator,
    DbContextOptions<DocumentDbContext> dbContextOptions) : IUpdateDocumentCommand
{
    /// <inheritdoc />
    public async Task UpdateAsync(DocumentInput modifiedInput,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await UpdateBusinessLogicAsync(modifiedInput, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e) when (e is DbUpdateException
                                      or ValidationException)
        {
            throw new InvalidArgumentCommandException(
                Errors.Errors.InvalidInput,
                e);
        }
        catch (Exception e) when (e is OperationCanceledException)
        {
            throw new CommandCancelledException(
                Errors.Errors.OperationCancelled,
                e);
        }
        catch (InvalidOperationException e)
        {
            throw new NoSuchItemCommandException(
                Errors.Errors.NoSuchItem,
                e);
        }
        catch (Exception e) when (e is DbUpdateConcurrencyException
                                      or not null)
        {
            throw new UnknownErrorScenarioException(
                Errors.Errors.UnexpectedError,
                e);
        }
    }

    private async Task UpdateBusinessLogicAsync(
        DocumentInput modifiedInput,
        CancellationToken cancellationToken = default)
    {
        await ValidateUpdateAsyncInput(modifiedInput).ConfigureAwait(false);
        Document mappedDocument = documentMapper.MapDocumentInputToDocument(modifiedInput);
        await UpdateAsyncDatabaseOperation(mappedDocument, cancellationToken).ConfigureAwait(false);
    }

    private async Task UpdateAsyncDatabaseOperation(Document documentInput,
        CancellationToken cancellationToken = default)
    {
        await using (DocumentDbContext ctx = new DocumentDbContext(dbContextOptions))
        {
            Document toBeModified = await ctx.Documents.FirstAsync(f => f.Id == documentInput.Id, cancellationToken)
                .ConfigureAwait(false);
            Modify(toBeModified, documentInput);
            // any tree belongs to Document will be managed at their own commands
            ctx.Entry(toBeModified).State = EntityState.Modified;
            await ctx.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    private void Modify(Document toBeModified, Document documentInput)
    {
        toBeModified.Name = documentInput.Name;
        toBeModified.Description = documentInput.Description;
        toBeModified.Uri = documentInput.Uri;
    }

    private async Task ValidateUpdateAsyncInput(DocumentInput modifiedInput)
    {
        if (modifiedInput is null)
        {
            string m = $"{nameof(modifiedInput)} cannot be null";
            throw new ArgumentNullException(m);
        }

        await documentInputValidator.ValidateAsync(modifiedInput, o =>
        {
            o.IncludeRuleSets(DocumentInputValidator.Scenarios.Update.ToString());
            o.ThrowOnFailures();
        });
    }
}