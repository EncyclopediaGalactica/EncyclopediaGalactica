namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.RelationType;

using EncyclopediaGalactica.Common.Contracts;
using FluentValidation;

public class DeleteRelationTypeScenarioInputValidator : AbstractValidator<RelationTypeInput>
{
    public DeleteRelationTypeScenarioInputValidator()
    {
        RuleFor(v => v.Id).GreaterThanOrEqualTo(1)
            .WithMessage($"The id of {nameof(RelationTypeInput)} entity must be greater or equal to 1.");
    }
}