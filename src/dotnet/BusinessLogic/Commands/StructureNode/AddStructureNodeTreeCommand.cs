namespace EncyclopediaGalactica.BusinessLogic.Commands.StructureNode;

using Contracts;
using Database;
using Entities;
using Exceptions;
using FluentValidation;
using Mappers;
using Microsoft.EntityFrameworkCore;
using Validators;

public class AddStructureNodeTreeCommand(
    IStructureNodeMapper mapper,
    DbContextOptions<DocumentDbContext> dbContextOptions,
    IValidator<StructureNode> validator) : IAddStructureNodeTreeCommand
{
    public async Task AddTreeAsync(StructureNodeInput structureNodeInput,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await AddNewRootNodeBusinessLogicAsync(structureNodeInput, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task AddNewRootNodeBusinessLogicAsync(
        StructureNodeInput structureNodeInput,
        CancellationToken cancellationToken = default)
    {
        ValidateProvidedInput(structureNodeInput);
        StructureNode structureNode = mapper.MapStructureNodeInputToStructureNode(structureNodeInput);
        await ValidateStructureEntity(structureNode, cancellationToken).ConfigureAwait(false);
        await AddTreeAsyncDatabaseOperation(structureNode, cancellationToken).ConfigureAwait(false);
    }

    private async Task AddTreeAsyncDatabaseOperation(
        StructureNode structureNode,
        CancellationToken cancellationToken = default)
    {
        await using DocumentDbContext ctx = new DocumentDbContext(dbContextOptions);
        // dfs to add the whole tree
    }

    private async Task ValidateStructureEntity(
        StructureNode structureNode,
        CancellationToken cancellationToken = default)
    {
        // todo: make the validator in a way it validates every item in the tree
        await validator.ValidateAsync(structureNode, o =>
        {
            o.IncludeRuleSets(Operations.Add);
            o.ThrowOnFailures();
        }, cancellationToken);
    }

    private void ValidateProvidedInput(StructureNodeInput structureNodeInput)
    {
        if (structureNodeInput is null)
        {
            string m = $"{nameof(structureNodeInput)} must not be null";
            throw new InvalidArgumentCommandException(m);
        }
    }
}