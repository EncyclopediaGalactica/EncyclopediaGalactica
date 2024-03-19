namespace EncyclopediaGalactica.BusinessLogic.Commands.StructureNode;

using Contracts;
using Database;
using Entities;
using Errors;
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
        catch (Exception e) when (e is ValidationException or InvalidArgumentCommandException)
        {
            string m = $"{nameof(AddNewStructureNodeCommand)} received invalid input.";
            throw new InvalidArgumentCommandException(m, e);
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

    private async Task AddNewBusinessLogicAsync(
        StructureNodeInput structureNodeInput,
        CancellationToken cancellationToken = default)
    {
        ValidateProvidedInput(structureNodeInput);
        StructureNode structureNode = structureNodeMapper.MapStructureNodeInputToStructureNode(structureNodeInput);
        await AddDatabaseOperationAsync(structureNode, cancellationToken).ConfigureAwait(false);
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