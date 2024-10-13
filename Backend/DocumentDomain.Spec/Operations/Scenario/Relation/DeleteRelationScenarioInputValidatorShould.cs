namespace DocumentDomain.Spec.Operations.Scenario.Relation;

using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Relation;
using FluentAssertions;
using FluentValidation.Results;

public class DeleteRelationScenarioInputValidatorShould
{
    private DeleteRelationScenarioInputValidator _validator = new();

    [Fact]
    public void ReturnInvalidState_WhenInputIsInvalid()
    {
        ValidationResult? result = _validator.Validate(new RelationInput { Id = 0 });
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void ReturnValidState_WhenInputIsInvalid()
    {
        ValidationResult? result = _validator.Validate(new RelationInput { Id = 1 });
        result.IsValid.Should().BeTrue();
    }
}