namespace EncyclopediaGalactica.BusinessLogic.Commands.StructureNode;

using Contracts;
using Database;
using Entities;
using Errors;
using Exceptions;
using Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class GetStructureNodeTreeCommand(
    IStructureNodeMapper mapper,
    DbContextOptions<DocumentDbContext> dbContextOptions,
    ILogger<IGetStructureNodeTreeCommand> logger) : IGetStructureNodeTreeCommand
{
    public async Task<StructureNodeResult> GetRootNodeByDocumentIdAsync(
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

    private async Task<StructureNodeResult> GetNodeBusinessLogicAsync(
        long documentId,
        CancellationToken cancellationToken = default)
    {
        ValidateInput(documentId);
        StructureNode structureNode = await GetNodeTreeAsyncDatabaseOperation(documentId, cancellationToken)
            .ConfigureAwait(false);
        return mapper.MapStructureNodeToStructureNodeResult(structureNode);
    }

    private async Task<StructureNode> GetNodeTreeAsyncDatabaseOperation(
        long documentId,
        CancellationToken cancellationToken = default)
    {
        await using DocumentDbContext ctx = new DocumentDbContext(dbContextOptions);
        return await ctx.StructureNodes
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