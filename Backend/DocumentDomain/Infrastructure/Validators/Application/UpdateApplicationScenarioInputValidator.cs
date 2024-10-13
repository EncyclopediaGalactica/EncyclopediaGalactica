namespace EncyclopediaGalactica.DocumentDomain.Infrastructure.Validators.Application;

using EncyclopediaGalactica.Common.Contracts;
using FluentValidation;

public class UpdateApplicationScenarioInputValidator : AbstractValidator<ApplicationInput>
{
    public UpdateApplicationScenarioInputValidator()
    {
        RuleFor(r => r.Id).GreaterThanOrEqualTo(1);
        RuleFor(r => r.Name.Trim().Length).GreaterThanOrEqualTo(3);
        RuleFor(r => r.Description.Trim().Length).GreaterThanOrEqualTo(3);
    }
}