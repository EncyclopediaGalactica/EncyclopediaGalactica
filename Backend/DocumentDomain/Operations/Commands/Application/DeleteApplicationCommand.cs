namespace EncyclopediaGalactica.DocumentDomain.Operations.Commands.Application;

using EncyclopediaGalactica.Common.Contracts;
using Entity;
using FluentValidation;
using Infrastructure.Database;
using Infrastructure.Validators.Application;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Scenarios.Application;

public class DeleteApplicationCommand(
    DeleteApplicationScenarioInputValidator validation,
    DbContextOptions<DocumentDomainDbContext> dbContextOptions)
{
    public async Task<Option<DeletionResult>> ExecuteAsync(
        DeleteApplicationScenarioContext ctx,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await ExecuteOperationAsync(ctx, cancellationToken).ConfigureAwait(false);
            return Option<DeletionResult>.Some(new DeletionResult());
        }
        catch (Exception e)
        {
            return Option<DeletionResult>.None;
        }
    }

    private async Task ExecuteOperationAsync(
        DeleteApplicationScenarioContext input, CancellationToken cancellationToken)
    {
        await ValidateInputAsync(input.Payload, cancellationToken).ConfigureAwait(false);
        await ExecuteDatabaseDatabaseOperationAsync(input.Payload!, cancellationToken).ConfigureAwait(false);
    }

    private async Task ExecuteDatabaseDatabaseOperationAsync(
        ApplicationInput inputPayload,
        CancellationToken cancellationToken)
    {
        await using DocumentDomainDbContext ctx = new(dbContextOptions);
        Application toBeDeleted = await ctx.Applications.FirstAsync(
            w => w.Id == inputPayload.Id, cancellationToken).ConfigureAwait(false);
        ctx.Entry(toBeDeleted).State = EntityState.Deleted;
        await ctx.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    private async Task ValidateInputAsync(
        ApplicationInput? inputPayload,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(inputPayload, nameof(inputPayload));
        await validation.ValidateAndThrowAsync(inputPayload, cancellationToken).ConfigureAwait(false);
    }
}