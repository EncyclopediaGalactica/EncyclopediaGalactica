namespace EncyclopediaGalactica.DocumentDomain.Operations.Commands.DocumentType;

using Common.Commands.Exceptions;
using EncyclopediaGalactica.Common.Contracts;
using FluentValidation;
using Infrastructure.Database;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Scenarios.DocumentType;

/// <summary>
///     Update the designated <see cref="DocumentType" /> entity in the database.
///     <remarks>
///         This command provides a single method Api to execute safely the update operation in the database.
///     </remarks>
/// </summary>
/// <param name="validator">The <see cref="UpdateDocumentTypeScenarioInputValidator" />.</param>
/// <param name="mapper">Mapper</param>
/// <param name="dbContextOptions">
///     <see cref="DbContextOptions{TContext}" />
/// </param>
public class UpdateDocumentTypeCommand(
    IValidator<DocumentTypeInput> validator,
    IDocumentTypeMapper mapper,
    DbContextOptions<DocumentDomainDbContext> dbContextOptions) : IUpdateDocumentTypeCommand
{
    public async Task<DocumentTypeResult> ExecuteAsync(
        DocumentTypeInput? input,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await ExecuteCommandAsync(input, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task<DocumentTypeResult> ExecuteCommandAsync(DocumentTypeInput? input,
        CancellationToken cancellationToken)
    {
        ValidateInput(input);
        Entity.DocumentType modification = mapper.FromDocumentTypeInput(input!);
        Entity.DocumentType modified = await ExecuteDatabaseOperationAsync(modification, cancellationToken)
            .ConfigureAwait(false);
        return mapper.ToDocumentTypeResult(modified);
    }

    private async Task<Entity.DocumentType> ExecuteDatabaseOperationAsync(
        Entity.DocumentType modification,
        CancellationToken cancellationToken)
    {
        await using DocumentDomainDbContext ctx = new(dbContextOptions);
        await using IDbContextTransaction tr = await ctx.Database.BeginTransactionAsync(cancellationToken)
            .ConfigureAwait(false);
        Entity.DocumentType? toBeModified = await ctx.DocumentTypes.FirstOrDefaultAsync(
            p => p.Id == modification.Id, cancellationToken).ConfigureAwait(false);

        if (toBeModified is null)
        {
            await tr.RollbackAsync(cancellationToken).ConfigureAwait(false);
            string msg = string.Format($"No {nameof(DocumentType)} entity with id: {modification.Id}.");
            throw new NoSuchItemCommandException(msg);
        }

        toBeModified.Name = modification.Name;
        toBeModified.Description = modification.Description;

        ctx.Entry(toBeModified).State = EntityState.Modified;
        await ctx.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        await tr.CommitAsync(cancellationToken).ConfigureAwait(false);
        return toBeModified;
    }

    private void ValidateInput(DocumentTypeInput? input)
    {
        ArgumentNullException.ThrowIfNull(input);
        validator.ValidateAndThrowAsync(input);
    }
}

public interface IUpdateDocumentTypeCommand
{
    Task<DocumentTypeResult> ExecuteAsync(DocumentTypeInput? input, CancellationToken cancellationToken = default);
}