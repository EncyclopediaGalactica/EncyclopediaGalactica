namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.RelationType;

using EncyclopediaGalactica.Common.Contracts;
using FluentValidation;

public class GetRelationTypeByIdScenarioInputValidator : AbstractValidator<RelationTypeInput>
{
    public GetRelationTypeByIdScenarioInputValidator()
    {
        RuleFor(r => r.Id).GreaterThanOrEqualTo(1)
            .WithMessage($"{nameof(RelationTypeInput)}.Id must be greater or equal to 1");
    }
}