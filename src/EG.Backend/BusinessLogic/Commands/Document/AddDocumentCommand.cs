namespace EncyclopediaGalactica.BusinessLogic.Commands.Document;

using Contracts;
using Database;
using Entities;
using Exceptions;
using FluentValidation;
using Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Validators;

public class AddDocumentCommand(
    IDocumentMapper documentMapper,
    IValidator<DocumentInput> documentInputValidator,
    DbContextOptions<DocumentDbContext> dbContextOptions,
    ILogger<AddDocumentCommand> logger) : IAddDocumentCommand
{
    /// <inheritdoc />
    public async Task<long> AddAsync(DocumentInput inputInput,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await AddBusinessLogicAsync(inputInput, cancellationToken);
        }
        catch (Exception e) when (e is ArgumentNullException
                                      or ValidationException
                                      or DbUpdateException)
        {
            throw new InvalidArgumentCommandException(
                Errors.Errors.InvalidInput,
                e);
        }
        catch (Exception e) when (e is OperationCanceledException)
        {
            throw new OperationCancelledCommandException(
                Errors.Errors.OperationCancelled,
                e);
        }
        catch (Exception e)
        {
            throw new UnknownErrorCommandException(
                Errors.Errors.UnexpectedError,
                e);
        }
    }

    private async Task<long> AddBusinessLogicAsync(DocumentInput input,
        CancellationToken cancellationToken)
    {
        await ValidationDocumentInputForAdding(input);
        Document document = documentMapper.MapDocumentInputToDocument(input);

        Document result = await AddAsyncDatabaseOperation(document, cancellationToken).ConfigureAwait(false);
        return result.Id;
    }

    private async Task<Document> AddAsyncDatabaseOperation(
        Document document,
        CancellationToken cancellationToken = default)
    {
        await using DocumentDbContext ctx = new DocumentDbContext(dbContextOptions);
        await ctx.Documents.AddAsync(document, cancellationToken).ConfigureAwait(false);
        await ctx.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        logger.LogDebug("Newly created {EntityName} id value: {IdValue}", nameof(Document), document.Id);

        return document;
    }

    private async Task ValidationDocumentInputForAdding(DocumentInput input)
    {
        ArgumentNullException.ThrowIfNull(input);

        await documentInputValidator.ValidateAsync(input, options =>
        {
            options.IncludeRuleSets(DocumentInputValidator.Scenarios.AddNew.ToString());
            options.ThrowOnFailures();
        });
    }
}