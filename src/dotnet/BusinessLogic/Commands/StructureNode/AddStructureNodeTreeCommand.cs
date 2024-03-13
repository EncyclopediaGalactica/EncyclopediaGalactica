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
    IValidator<StructureNodeInput> validator) : IAddStructureNodeTreeCommand
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
        await ValidateProvidedInput(structureNodeInput, cancellationToken).ConfigureAwait(false);
        StructureNode structureNode = mapper.MapStructureNodeInputToStructureNode(structureNodeInput);
        await AddTreeAsyncDatabaseOperation(structureNode, cancellationToken).ConfigureAwait(false);
    }

    private async Task AddTreeAsyncDatabaseOperation(
        StructureNode structureNode,
        CancellationToken cancellationToken = default)
    {
        await using DocumentDbContext ctx = new DocumentDbContext(dbContextOptions);
        // dfs to add the whole tree
    }

    private async Task ValidateProvidedInput(StructureNodeInput structureNodeInput,
        CancellationToken cancellationToken = default)
    {
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