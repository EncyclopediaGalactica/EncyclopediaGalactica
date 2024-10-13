namespace DocumentDomain.Spec.Operations.Scenario.Application;

using Data;
using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Infrastructure.Validators.Application;
using FluentAssertions;
using FluentValidation;

public class UpdateApplicationScenarioInputValidatorShould
{
    private readonly UpdateApplicationScenarioInputValidator _validator = new();

    [Theory]
    [ClassData(typeof(UpdateApplicationScenarioInvalidInputData))]
    public void Throw_WhenInputIsInvalid(ApplicationInput input)
    {
        Func<Task> f = async () => { await _validator.ValidateAndThrowAsync(input); };

        f.Should().ThrowExactlyAsync<ValidationException>();
    }

    [Fact]
    public void NotThrow_WhenInputIsValid()
    {
        Func<Task> f = async () =>
        {
            await _validator.ValidateAndThrowAsync(
                new ApplicationInput
                {
                    Id = 1,
                    Name = "name",
                    Description = "Desc"
                });
        };
        f.Should().NotThrowAsync();
    }
}