#region

#endregion

namespace EncyclopediaGalactica.DocumentDomain.Operations.Commands;

using Common.Commands;
using Common.Commands.Exceptions;
using EncyclopediaGalactica.Common.Contracts;
using Entity;
using Infrastructure.Database;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class GetStructureNodeTreeCommand(
    IDocumentStructureNodeMapper mapper,
    DbContextOptions<DocumentDomainDbContext> dbContextOptions,
    ILogger<IGetStructureNodeTreeCommand> logger) : IGetStructureNodeTreeCommand
{
    public async Task<DocumentStructureNodeInput> GetRootNodeByDocumentIdAsync(
        long documentId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await GetNodeBusinessLogicAsync(documentId, cancellationToken).ConfigureAwait(false);
        }
        catch (OperationCanceledException e)
        {
            throw new OperationCancelledCommandException(Errors.OperationCancelled, e);
        }
        catch (Exception e) when (e is ArgumentNullException or InvalidOperationException)
        {
            throw new InvalidArgumentCommandException(Errors.InvalidInput, e);
        }
        catch (Exception e)
        {
            string m = $"Unknown error happened!";
            throw new UnknownErrorCommandException(m, e);
        }
    }

    private async Task<DocumentStructureNodeInput> GetNodeBusinessLogicAsync(
        long documentId,
        CancellationToken cancellationToken = default)
    {
        ValidateInput(documentId);
        DocumentStructureNode documentStructureNode =
            await GetNodeTreeAsyncDatabaseOperation(documentId, cancellationToken)
                .ConfigureAwait(false);
        return mapper.MapStructureNodeToStructureNodeResult(documentStructureNode);
    }

    private async Task<DocumentStructureNode> GetNodeTreeAsyncDatabaseOperation(
        long documentId,
        CancellationToken cancellationToken = default)
    {
        await using DocumentDomainDbContext ctx = new DocumentDomainDbContext(dbContextOptions);
        return await ctx.DocumentStructureNodes
            .FirstAsync(f => f.DocumentId == documentId && f.IsRootNode == 1, cancellationToken);
    }

    private void ValidateInput(long id)
    {
        long notAllowedValue = 0;
        if (id == notAllowedValue)
        {
            string m = $"{nameof(id)} cannot be {notAllowedValue}";
            throw new InvalidArgumentCommandException(m);
        }
    }
}