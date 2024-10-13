namespace DocumentDomain.Spec.Operations.Scenario.Application.Data;

using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Infrastructure.Validators.Application;
using FluentAssertions;
using FluentValidation;

public class DeleteApplicationScenarioInputValidatorShould
{
    private readonly DeleteApplicationScenarioInputValidator _validator = new();

    [Fact]
    public async Task Throw_WhenInputIsInvalid()
    {
        Func<Task> f = async () => { await _validator.ValidateAndThrowAsync(new ApplicationInput { Id = 0 }); };
        await f.Should().ThrowExactlyAsync<ValidationException>();
    }
}