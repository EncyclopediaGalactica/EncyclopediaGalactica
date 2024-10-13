namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Filetype;

using EncyclopediaGalactica.Common.Contracts;
using FluentValidation;

public class DeleteFiletypeScenarioInputValidator : AbstractValidator<FiletypeInput>
{
    public DeleteFiletypeScenarioInputValidator()
    {
        RuleFor(r => r.Id)
            .GreaterThanOrEqualTo(1)
            .WithMessage($"The id of {nameof(Filetype)} entity must be greater or equal to 1.");
    }
}