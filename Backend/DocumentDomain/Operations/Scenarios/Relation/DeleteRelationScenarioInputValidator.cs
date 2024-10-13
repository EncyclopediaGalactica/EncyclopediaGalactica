namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Relation;

using EncyclopediaGalactica.Common.Contracts;
using FluentValidation;

public class DeleteRelationScenarioInputValidator : AbstractValidator<RelationInput>
{
    public DeleteRelationScenarioInputValidator()
    {
        RuleFor(r => r.Id)
            .GreaterThanOrEqualTo(1)
            .WithMessage($"Id value must be greater or equal to 1.");
    }
}