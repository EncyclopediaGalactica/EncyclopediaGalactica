namespace EncyclopediaGalactica.DocumentDomain.Operations.Commands.DocumentType;

using Common.Commands;
using EncyclopediaGalactica.Common.Contracts;
using FluentValidation;
using Infrastructure.Database;
using Infrastructure.Mappers;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

/// <summary>
///     Get the designated <see cref="DocumentType" /> by Id command.
/// </summary>
/// <remarks>
///     This command provides a single method Api to retrieve safely the desired <see cref="DocumentType" /> entity.
/// </remarks>
/// <param name="documentTypeMapper"><see cref="IDocumentTypeMapper" /> implementation.</param>
/// <param name="dbContextOptions"><see cref="DbContextOptions{TContext}" />.</param>
public class GetDocumentTypeByIdCommand(
    IDocumentTypeMapper documentTypeMapper,
    DbContextOptions<DocumentDomainDbContext> dbContextOptions) : IGetDocumentTypeByIdCommand
{
    public async Task<Option<DocumentTypeResult>> ExecuteAsync(long ctx,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await ExecuteOperationAsync(ctx, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return Option<DocumentTypeResult>.None;
        }
    }

    private async Task<DocumentTypeResult> ExecuteOperationAsync(long id, CancellationToken cancellationToken)
    {
        ValidateInput(id);
        Entity.DocumentType result = await ExecuteDatabaseOperation(id, cancellationToken).ConfigureAwait(false);
        return documentTypeMapper.ToDocumentTypeResult(result);
    }

    private async Task<Entity.DocumentType> ExecuteDatabaseOperation(long id, CancellationToken cancellationToken)
    {
        await using DocumentDomainDbContext ctx = new(dbContextOptions);
        return await ctx.DocumentTypes.FirstAsync(p => p.Id == id, cancellationToken).ConfigureAwait(false);
    }

    private void ValidateInput(long id)
    {
        if (id <= 0)
        {
            string msg = $"{nameof(DocumentTypeInput)} Id value cannot be be smaller than 1.";
            throw new ValidationException(msg);
        }
    }
}

public interface IGetDocumentTypeByIdCommand : IHaveInputAndResultCommand<long, DocumentTypeResult>
{
}