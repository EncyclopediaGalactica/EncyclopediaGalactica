namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Relation;

using EncyclopediaGalactica.Common.Contracts;
using FluentValidation;

public class AddRelationScenarioInputValidator : AbstractValidator<RelationInput>
{
    public AddRelationScenarioInputValidator()
    {
        RuleFor(r => r.Id)
            .Equal(0)
            .WithMessage($"Id must be zero when creating {nameof(RelationInput)} entity.");

        RuleFor(r => r.LeftEndId)
            .GreaterThanOrEqualTo(1)
            .WithMessage($"The left end of a {nameof(RelationInput)} entity must be greater or equal to 0.");


        RuleFor(r => r.RightEndId)
            .GreaterThanOrEqualTo(1)
            .WithMessage($"The right end of a {nameof(RelationInput)} entity must be greater or equal to 0.");
    }
}