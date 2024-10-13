namespace EncyclopediaGalactica.DocumentDomain.Infrastructure.Validators.Application;

using EncyclopediaGalactica.Common.Contracts;
using FluentValidation;

public class AddApplicationScenarioInputValidator : AbstractValidator<ApplicationInput>
{
    public AddApplicationScenarioInputValidator()
    {
        RuleFor(r => r.Id).Equal(0);
        RuleFor(r => r.Name.Trim().Length).GreaterThanOrEqualTo(3);
        RuleFor(r => r.Description.Trim().Length).GreaterThanOrEqualTo(3);
    }
}