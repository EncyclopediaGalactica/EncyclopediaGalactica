namespace DocumentDomain.Spec.Operations.Scenario.Application;

using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Infrastructure.Validators.Application;
using FluentAssertions;
using FluentValidation;

public class DeleteApplicationScenarioValidatorShould
{
    private readonly DeleteApplicationScenarioInputValidator _validator = new();

    [Fact]
    public async Task Throw_WhenInputIsInvalid()
    {
        Func<Task> f = async () => { await _validator.ValidateAndThrowAsync(new ApplicationInput { Id = 0 }); };
        await f.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task NotThrow_WhenInputValid()
    {
        Func<Task> f = async () => { await _validator.ValidateAndThrowAsync(new ApplicationInput { Id = 4 }); };
        await f.Should().NotThrowAsync<ValidationException>();
    }
}