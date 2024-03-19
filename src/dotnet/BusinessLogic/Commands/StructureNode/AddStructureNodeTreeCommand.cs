namespace EncyclopediaGalactica.BusinessLogic.Commands.StructureNode;

using Contracts;
using Database;
using Entities;
using Errors;
using Exceptions;
using FluentValidation;
using Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Validators;

public class AddStructureNodeTreeCommand(
    IStructureNodeMapper mapper,
    DbContextOptions<DocumentDbContext> dbContextOptions,
    IValidator<StructureNodeInput> validator,
    ILogger<AddStructureNodeTreeCommand> logger) : IAddStructureNodeTreeCommand
{
    public async Task AddTreeAsync(
        long documentId,
        StructureNodeInput structureNodeInput,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await AddNewRootNodeBusinessLogicAsync(documentId, structureNodeInput, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (ValidationException e)
        {
            throw new InvalidArgumentCommandException(Errors.InvalidInput, e);
        }
        catch (OperationCanceledException e)
        {
            throw new OperationCancelledCommandException(Errors.OperationCancelled, e);
        }
        catch (Exception e)
        {
            throw new UnknownErrorCommandException(Errors.UnexpectedError, e);
        }
    }

    private async Task AddNewRootNodeBusinessLogicAsync(
        long documentId,
        StructureNodeInput structureNodeInput,
        CancellationToken cancellationToken = default)
    {
        OverWriteRootNode(structureNodeInput);
        OverwriteStructureNodesDocumentIdValue(structureNodeInput, documentId);
        await ValidateProvidedInput(documentId, structureNodeInput, cancellationToken).ConfigureAwait(false);
        StructureNode structureNode = mapper.MapStructureNodeInputToStructureNode(structureNodeInput);
        await AddTreeAsyncDatabaseOperation(structureNode, cancellationToken).ConfigureAwait(false);
    }

    private async Task AddTreeAsyncDatabaseOperation(
        StructureNode structureNode,
        CancellationToken cancellationToken = default)
    {
        await using DocumentDbContext ctx = new DocumentDbContext(dbContextOptions);
        await ctx.StructureNodes.AddAsync(structureNode, cancellationToken).ConfigureAwait(false);
        await ctx.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    private void OverWriteRootNode(StructureNodeInput input)
    {
        input.IsRootNode = 1;
    }

    private void OverwriteStructureNodesDocumentIdValue(StructureNodeInput structureNodeInput, long documentId)
    {
        structureNodeInput.DocumentId = documentId;
    }

    private async Task ValidateProvidedInput(
        long documentId,
        StructureNodeInput structureNodeInput,
        CancellationToken cancellationToken = default)
    {
        const long notAllowedValue = 0;
        if (documentId == notAllowedValue)
        {
            string m = $"{nameof(documentId)} cannot be {notAllowedValue}";
            throw new InvalidArgumentCommandException(m);
        }

        if (structureNodeInput is null)
        {
            string m = $"{nameof(structureNodeInput)} must not be null";
            throw new InvalidArgumentCommandException(m);
        }

        // todo: make the validator in a way it validates every item in the tree
        await validator.ValidateAsync(structureNodeInput, o =>
        {
            o.IncludeRuleSets(Operations.Add);
            o.ThrowOnFailures();
        }, cancellationToken);
    }
}