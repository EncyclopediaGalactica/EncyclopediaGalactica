namespace EncyclopediaGalactica.DocumentDomain.Infrastructure.Validators.Application;

using EncyclopediaGalactica.Common.Contracts;
using FluentValidation;

public class GetApplicationByIdValidator : AbstractValidator<ApplicationInput>
{
    public GetApplicationByIdValidator()
    {
        RuleFor(f => f.Id).GreaterThanOrEqualTo(0);
    }
}