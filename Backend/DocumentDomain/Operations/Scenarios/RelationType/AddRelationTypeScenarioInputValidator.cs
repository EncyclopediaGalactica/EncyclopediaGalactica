namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.RelationType;

using EncyclopediaGalactica.Common.Contracts;
using FluentValidation;

public class AddRelationTypeScenarioInputValidator : AbstractValidator<RelationTypeInput>
{
    public AddRelationTypeScenarioInputValidator()
    {
        RuleFor(r => r.Id)
            .Equal(0)
            .WithMessage($"The id of {nameof(RelationType)} entity must be zero during creation.");

        RuleFor(r => r.Name)
            .NotNull()
            .WithMessage($"The name of {nameof(RelationType)} entity must not be null.");

        When(w => w.Name is not null, () =>
        {
            RuleFor(w => w.Name)
                .NotEmpty()
                .WithMessage($"The name of {nameof(RelationType)} entity must not be empty");
            RuleFor(w => w.Name.Trim().Length)
                .GreaterThanOrEqualTo(3)
                .WithMessage($"The name length of {nameof(RelationType)} must be equal or greater to 3 chars.");
        });

        RuleFor(r => r.Description)
            .NotNull()
            .WithMessage($"The description of {nameof(RelationType)} entity must not be null.");

        When(w => w.Description is not null, () =>
        {
            RuleFor(w => w.Description)
                .NotEmpty()
                .WithMessage($"The description of {nameof(RelationType)} entity must not be empty");
            RuleFor(w => w.Description.Trim().Length)
                .GreaterThanOrEqualTo(3)
                .WithMessage($"The description length of {nameof(RelationType)} must be equal or greater to 3 chars.");
        });
    }
}