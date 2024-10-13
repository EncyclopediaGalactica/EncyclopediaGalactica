namespace EncyclopediaGalactica.DocumentDomain.Operations.Commands;

using Common.Commands;
using Common.Commands.Exceptions;
using Common.Validation;
using EncyclopediaGalactica.Common.Contracts;
using Entity;
using FluentValidation;
using Infrastructure.Database;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

public class AddNewStructureNodeCommand(
    IDocumentStructureNodeMapper documentStructureNodeMapper,
    DbContextOptions<DocumentDomainDbContext> dbContextOptions,
    IValidator<DocumentStructureNodeInput> validator) : IAddNewStructureNodeCommand
{
    public async Task AddNewAsync(DocumentStructureNodeInput structureNodeInput,
        CancellationToken cancellationToken = default)
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
        DocumentStructureNodeInput structureNodeInput,
        CancellationToken cancellationToken = default)
    {
        ValidateProvidedInput(structureNodeInput);
        DocumentStructureNode documentStructureNode =
            documentStructureNodeMapper.MapStructureNodeInputToStructureNode(structureNodeInput);
        await AddDatabaseOperationAsync(documentStructureNode, cancellationToken).ConfigureAwait(false);
    }

    private async Task<DocumentStructureNode> AddDatabaseOperationAsync(DocumentStructureNode documentStructureNode,
        CancellationToken cancellationToken = default)
    {
        await using DocumentDomainDbContext ctx = new DocumentDomainDbContext(dbContextOptions);
        await ctx.DocumentStructureNodes.AddAsync(documentStructureNode, cancellationToken).ConfigureAwait(false);
        return documentStructureNode;
    }

    private void ValidateProvidedInput(DocumentStructureNodeInput structureNodeInput)
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