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
using Microsoft.Extensions.Logging;

public class AddApplicationCommand(
    IApplicationMapper applicationMapper,
    AddApplicationScenarioInputValidator validator,
    DbContextOptions<DocumentDomainDbContext> dbContextOptions,
    ILogger<AddApplicationCommand> logger) : IAddApplicationCommand
{
    public async Task<Option<ApplicationResult>> ExecuteAsync(
        IHavePayloadScenarioContext<ApplicationInput> ctx,
        CancellationToken cancellationToken = default)
    {
        try
        {
            ApplicationResult result = await ExecuteCommandAsync(ctx.Payload!, cancellationToken)
                .ConfigureAwait(false);
            return Option<ApplicationResult>.Some(result);
        }
        catch (Exception e)
        {
            logger.LogError(
                "Correlation id: {CorrelationId}" +
                "Error happened while executing {CommandName} command. " +
                "Further details: {ExceptionMessage}",
                ctx.CorrelationId,
                nameof(AddApplicationCommand),
                e.Message);
            return Option<ApplicationResult>.None;
        }
    }

    private async Task<ApplicationResult> ExecuteCommandAsync(
        ApplicationInput applicationInput,
        CancellationToken cancellationToken)
    {
        await ValidateInputAsync(applicationInput, cancellationToken).ConfigureAwait(false);
        Application application = applicationMapper.FromApplicationInput(applicationInput);
        Application recorded = await RecordEntityAsync(application, cancellationToken).ConfigureAwait(false);
        return applicationMapper.ToApplicationResult(recorded);
    }

    private async Task ValidateInputAsync(ApplicationInput applicationInput, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(applicationInput, cancellationToken).ConfigureAwait(false);
    }

    private async Task<Application> RecordEntityAsync(
        Application application,
        CancellationToken cancellationToken)
    {
        await using DocumentDomainDbContext ctx = new(dbContextOptions);
        await ctx.Applications.AddAsync(application, cancellationToken).ConfigureAwait(false);
        await ctx.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return application;
    }
}

/// Add Application Command Interface
public interface IAddApplicationCommand :
    IHaveInputAndResultCommand<IHavePayloadScenarioContext<ApplicationInput>, ApplicationResult>
{
}