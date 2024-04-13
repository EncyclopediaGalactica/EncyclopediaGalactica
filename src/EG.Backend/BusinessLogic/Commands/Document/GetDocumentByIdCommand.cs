namespace EncyclopediaGalactica.BusinessLogic.Commands.Document;

using Contracts;
using Database;
using Entities;
using Exceptions;
using Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class GetDocumentByIdCommand(
    IDocumentMapper documentMapper,
    DbContextOptions<DocumentDbContext> dbContextOptions,
    ILogger<GetDocumentByIdCommand> logger) : IGetDocumentByIdCommand
{
    /// <inheritdoc />
    public async Task<DocumentResult> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await GetByIdBusinessLogicAsync(id, cancellationToken).ConfigureAwait(false);
        }
        catch (InvalidOperationException e)
        {
            throw new NoSuchItemCommandException(
                Errors.Errors.NoSuchItem,
                e);
        }
        catch (OperationCanceledException e)
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

    private async Task<DocumentResult> GetByIdBusinessLogicAsync(long id, CancellationToken cancellationToken)
    {
        ValidateInput(id);
        Document result = await GetByIdAsyncDatabaseOperation(id, cancellationToken).ConfigureAwait(false);
        DocumentResult input = documentMapper.MapDocumentToDocumentResult(result);
        return input;
    }

    private async Task<Document> GetByIdAsyncDatabaseOperation(long id, CancellationToken cancellationToken = default)
    {
        await using DocumentDbContext ctx = new DocumentDbContext(dbContextOptions);
        return await ctx.Documents.FirstAsync(f => f.Id == id, cancellationToken).ConfigureAwait(false);
    }

    private void ValidateInput(long id)
    {
        logger.LogDebug("Input value: {InputValue}", id);
        const long notAllowedValue = 0;
        if (id == notAllowedValue)
        {
            string m = $"{id} cannot be {notAllowedValue}";
            throw new InvalidArgumentCommandException(m);
        }
    }
}