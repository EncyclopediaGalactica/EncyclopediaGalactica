namespace DocumentDomain.Spec.Operations.Scenario.Application;

using Data;
using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Application;
using FluentAssertions;
using LanguageExt;

public class AddApplicationScenarioShould : ScenarioBaseTest
{
  [Theory]
  [ClassData(typeof(AddApplicationScenarioInvalidInputData))]
  public async Task Return_None_WhenInputIsInvalid(ApplicationInput input)
  {
    AddApplicationScenarioContext ctx = new AddApplicationScenarioContext
    {
      CorrelationId = Guid.NewGuid(),
      Payload = input

    };
    Option<ApplicationResult> result = await AddApplicationScenario.ExecuteAsync(ctx);
    result.IsNone.Should().BeTrue();

  }

  [Fact]
  public async Task Return_Some_WhenEntityHasBeenCreated()
  {
    AddApplicationScenarioContext ctx = new AddApplicationScenarioContext
    {
      CorrelationId = Guid.NewGuid(),
      Payload = new ApplicationInput
      {
        Id = 0,
        Name = "name",
        Description = "description"
      }
    };
    Option<ApplicationResult> result = await AddApplicationScenario.ExecuteAsync(ctx);

    result.IsNone.Should().BeFalse();
    result.IsSome.Should().BeTrue();
    result.IfSome(some =>
    {
      some.Id.Should().BeGreaterOrEqualTo(1L);
      some.Name.Should().Be(ctx.Payload.Name);
      some.Description.Should().Be(ctx.Payload.Description);
    });
  }
}