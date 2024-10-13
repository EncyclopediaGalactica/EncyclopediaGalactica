namespace DocumentDomain.Spec.Operations.Scenario.Filetype;

using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Filetype;
using FluentAssertions;
using FluentValidation.Results;

public class DeleteFiletypeScenarioInputValidatorShould
{
    private DeleteFiletypeScenarioInputValidator validator = new();

    [Fact]
    public void ShowInvalidState_WhenTheInputIsInvalid()
    {
        ValidationResult result = validator.Validate(new FiletypeInput { Id = 0 });
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
    }

    [Fact]
    public void ShowValidState_WhenTheInputIsValid()
    {
        ValidationResult result = validator.Validate(new FiletypeInput { Id = 1 });
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }
}