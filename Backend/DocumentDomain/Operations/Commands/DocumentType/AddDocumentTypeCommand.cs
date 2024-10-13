namespace EncyclopediaGalactica.DocumentDomain.Operations.Commands.DocumentType;

using Common.Commands;
using EncyclopediaGalactica.Common.Contracts;
using Entity;
using FluentValidation;
using Infrastructure.Database;
using Infrastructure.Mappers;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Scenarios.DocumentType;

/// <summary>
///     Add Document Type Command interface.
///     <remarks>
///         It provides a single method to execute the command adding a new <see cref="DocumentType" /> entity to the
///         system.
///     </remarks>
/// </summary>
/// <param name="validator"><see cref="AddDocumentTypeScenarioInputValidator" /> instance.</param>
/// <param name="documentTypeMapper"><see cref="IDocumentTypeMapper" /> implementation.</param>
/// <param name="dbContextOptions"><see cref="DbContextOptions{TContext}" />.</param>
public class AddDocumentTypeCommand(
    AddDocumentTypeScenarioInputValidator validator,
    IDocumentTypeMapper documentTypeMapper,
    DbContextOptions<DocumentDomainDbContext> dbContextOptions) : IAddDocumentTypeCommand
{
    /// <inheritdoc />
    public async Task<Option<DocumentTypeResult>> ExecuteAsync(
        DocumentTypeInput? ctx,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await ExecuteOperationAsync(ctx, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task<Option<DocumentTypeResult>> ExecuteOperationAsync(
        DocumentTypeInput? input,
        CancellationToken
            cancellationToken)
    {
        ValidateInput(input);
        DocumentType toBeRecorded = documentTypeMapper.FromDocumentTypeInput(input);
        DocumentType result =
            await ExecuteDatabaseOperation(toBeRecorded, cancellationToken).ConfigureAwait(false);
        return documentTypeMapper.ToDocumentTypeResult(result);
    }

    private async Task<DocumentType> ExecuteDatabaseOperation(DocumentType toBeRecorded,
        CancellationToken cancellationToken)
    {
        await using DocumentDomainDbContext ctx = new DocumentDomainDbContext(dbContextOptions);
        ctx.DocumentTypes.Add(toBeRecorded);
        await ctx.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return toBeRecorded;
    }

    private void ValidateInput(DocumentTypeInput? input)
    {
        ArgumentNullException.ThrowIfNull(input);

        validator.ValidateAndThrow(input);
    }
}

public interface IAddDocumentTypeCommand : IHaveInputAndResultCommand<DocumentTypeInput, DocumentTypeResult>
{
}