namespace EncyclopediaGalactica.DocumentDomain.Operations.Commands.Application;

using Common.Commands;
using Common.Scenario;
using EncyclopediaGalactica.Common.Contracts;
using Entity;
using FluentValidation;
using Infrastructure.Database;
using Infrastructure.Mappers;
using Infrastructure.Validators.Application;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

public class UpdateApplicationCommand(
    IApplicationMapper applicationMapper,
    UpdateApplicationScenarioInputValidator validator,
    DbContextOptions<DocumentDomainDbContext> dbContextOptions)
    : IUpdateApplicationCommand
{
    public async Task<Option<ApplicationResult>> ExecuteAsync(IHavePayloadScenarioContext<ApplicationInput> ctx,
        CancellationToken cancellationToken = default)
    {
        try
        {
            ApplicationResult result = await ExecuteOperationAsync(
                ctx,
                cancellationToken
            ).ConfigureAwait(false);
            return Option<ApplicationResult>.Some(result);
        }
        catch (Exception e)
        {
            return Option<ApplicationResult>.None;
        }
    }

    private async Task<ApplicationResult> ExecuteOperationAsync(IHavePayloadScenarioContext<ApplicationInput> ctx,
        CancellationToken cancellationToken)
    {
        await ValidateInputAsync(ctx.Payload, cancellationToken).ConfigureAwait(false);
        Application application = applicationMapper.FromApplicationInput(ctx.Payload!);
        Application savedResult = await ExecuteDatabaseOperationAsync(application, cancellationToken)
            .ConfigureAwait(false);
        return applicationMapper.ToApplicationResult(savedResult);
    }

    private async Task<Application> ExecuteDatabaseOperationAsync(Application input,
        CancellationToken cancellationToken)
    {
        await using DocumentDomainDbContext ctx = new(dbContextOptions);
        Application toBeUpdated = await ctx.Applications.FirstAsync(
            p => p.Id == input.Id, cancellationToken).ConfigureAwait(false);

        toBeUpdated.Name = input.Name;
        toBeUpdated.Description = input.Description;

        ctx.Entry(toBeUpdated).State = EntityState.Modified;
        await ctx.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return toBeUpdated;
    }

    private async Task ValidateInputAsync(ApplicationInput? payload, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(payload);

        await validator.ValidateAndThrowAsync(payload, cancellationToken).ConfigureAwait(false);
    }
}

public interface IUpdateApplicationCommand : IHaveInputAndResultCommand<
    IHavePayloadScenarioContext<ApplicationInput>, ApplicationResult>
{
}