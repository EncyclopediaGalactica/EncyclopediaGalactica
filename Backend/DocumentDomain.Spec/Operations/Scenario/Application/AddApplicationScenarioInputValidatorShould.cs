namespace DocumentDomain.Spec.Operations.Scenario.Application;

using Data;
using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Infrastructure.Validators.Application;
using FluentAssertions;
using FluentValidation;

public class AddApplicationScenarioInputValidatorShould
{
    private readonly AddApplicationScenarioInputValidator _validator = new();

    [Theory]
    [ClassData(typeof(AddApplicationScenarioInvalidInputData))]
    public void Throw_WhenInputIsInvalid(ApplicationInput input)
    {
        Func<Task> f = async () => { await _validator.ValidateAndThrowAsync(input); };

        f.Should().ThrowExactlyAsync<ValidationException>();
    }
}