namespace EncyclopediaGalactica.BusinessLogic.Commands.StructureNode;

using Contracts;
using Database;
using Entities;
using Exceptions;
using FluentValidation;
using Mappers;
using Microsoft.EntityFrameworkCore;
using Validators;

public class AddNewStructureNodeCommand(
    IStructureNodeMapper structureNodeMapper,
    DbContextOptions<DocumentDbContext> dbContextOptions,
    IValidator<StructureNodeInput> validator) : IAddNewStructureNodeCommand
{
    public async Task AddNewAsync(StructureNodeInput structureNodeInput, CancellationToken cancellationToken = default)
    {
        try
        {
            await AddNewBusinessLogicAsync(structureNodeInput, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task AddNewBusinessLogicAsync(
        StructureNodeInput structureNodeInput,
        CancellationToken cancellationToken = default)
    {
        ValidateProvidedInput(structureNodeInput);
        StructureNode structureNode = structureNodeMapper.MapStructureNodeInputToStructureNode(structureNodeInput);
        StructureNode newStructureNode = await AddDatabaseOperationAsync(structureNode, cancellationToken)
            .ConfigureAwait(false);
    }

    private async Task<StructureNode> AddDatabaseOperationAsync(StructureNode structureNode,
        CancellationToken cancellationToken = default)
    {
        await using DocumentDbContext ctx = new DocumentDbContext(dbContextOptions);
        await ctx.StructureNodes.AddAsync(structureNode, cancellationToken).ConfigureAwait(false);
        return structureNode;
    }

    private void ValidateProvidedInput(StructureNodeInput structureNodeInput)
    {
        if (structureNodeInput is null)
        {
            string m = $"{nameof(structureNodeInput)} cannot be null";
            throw new InvalidArgumentCommandException(m);
        }

        validator.ValidateAsync(structureNodeInput, o =>
        {
            o.IncludeRuleSets(Operations.Add);
            o.ThrowOnFailures();
        });
    }
}