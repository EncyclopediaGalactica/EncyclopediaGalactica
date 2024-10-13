namespace EncyclopediaGalactica.DocumentDomain.Operations.Commands.Application;

using EncyclopediaGalactica.Common.Contracts;
using Entity;
using FluentValidation;
using Infrastructure.Database;
using Infrastructure.Mappers;
using Infrastructure.Validators.Application;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Scenarios.Application;

public class GetApplicationByIdCommand(
    ApplicationMapper applicationMapper,
    GetApplicationByIdValidator validator,
    DbContextOptions<DocumentDomainDbContext> dbContextOptions)
{
    public async Task<Option<ApplicationResult>> ExecuteAsync(
        GetApplicationByIdScenarioContext ctx,
        CancellationToken cancellationToken = default
    )
    {
        if (!InputValidation(ctx))
        {
            return Option<ApplicationResult>.None;
        }

        Application byId = await ExecuteDatabaseOperationAsyn(ctx.Payload, cancellationToken).ConfigureAwait(false);
        ApplicationResult mappedResult = applicationMapper.ToApplicationResult(byId);
        return Option<ApplicationResult>.Some(mappedResult);
    }

    private async Task<Application> ExecuteDatabaseOperationAsyn(
        ApplicationInput ctxPayload,
        CancellationToken cancellationToken = default)
    {
        await using DocumentDomainDbContext ctx = new(dbContextOptions);
        Application result = await ctx.Applications.FirstAsync(
            f => f.Id == ctxPayload.Id,
            cancellationToken).ConfigureAwait(false);
        return result;
    }

    private bool InputValidation(GetApplicationByIdScenarioContext ctx)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(ctx);
            ArgumentNullException.ThrowIfNull(ctx.Payload);
            validator.ValidateAndThrow(ctx.Payload);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}