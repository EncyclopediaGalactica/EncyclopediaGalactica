namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Filetype;

using EncyclopediaGalactica.Common.Contracts;
using FluentValidation;

public class GetFiletypeByIdScenarioInputValidator : AbstractValidator<FiletypeInput>
{
    public GetFiletypeByIdScenarioInputValidator()
    {
        RuleFor(r => r.Id)
            .GreaterThanOrEqualTo(1)
            .WithMessage($"The id of {nameof(FiletypeInput)} entity must be greater or equals to 1.");
    }
}