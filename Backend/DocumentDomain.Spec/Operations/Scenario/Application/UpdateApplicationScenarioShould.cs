namespace DocumentDomain.Spec.Operations.Scenario.Application;

using Data;
using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Application;
using FluentAssertions;
using LanguageExt;

public class UpdateApplicationScenarioShould : ScenarioBaseTest
{
  [Theory]
  [ClassData(typeof(UpdateApplicationScenarioInvalidInputData))]
  public async Task Return_NoneWhenInputIsInvalid(ApplicationInput input)
  {
    Option<ApplicationResult> result = await UpdateApplicationScenario.ExecuteAsync(
        new UpdateApplicationScenarioContext
        {
          Payload = input,
          CorrelationId = Guid.NewGuid()
        });
    result.IsNone.Should().BeTrue();
  }

  [Fact]
  public async Task Return_SomeWhenInputIsValid()
  {
    ApplicationInput input = new ApplicationInput
    {
      Id = 0,
      Name = "initial name",
      Description = "initial desc"
    };
    Option<ApplicationResult> initialResult = await AddApplicationScenario.ExecuteAsync(
        new AddApplicationScenarioContext
        {
          Payload = input,
          CorrelationId = Guid.NewGuid()
        });
    initialResult.IsSome.Should().BeTrue();
    ApplicationInput updateItem = new();
    initialResult.IfSome(e =>
    {
      updateItem.Id = e.Id;
      updateItem.Name = $"updated {e.Name}";
      updateItem.Description = $"updated {e.Description}";
    });
    Option<ApplicationResult> updateResult = await UpdateApplicationScenario.ExecuteAsync(
        new UpdateApplicationScenarioContext
        {
          CorrelationId = Guid.NewGuid(),
          Payload = updateItem
        });
    updateResult.IsSome.Should().BeTrue();
    updateResult.IfSome(result =>
    {
      result.Id.Should().Be(updateItem.Id);
      result.Name.Should().Be(updateItem.Name);
      result.Description.Should().Be(updateItem.Description);
    });
  }

  [Fact]
  public async Task Return_NoneWhenThereIsNoSuchObjec()
  {
    ApplicationInput input = new ApplicationInput
    {
      Id = 0,
      Name = "initial name",
      Description = "initial desc"
    };
    Option<ApplicationResult> initialResult = await AddApplicationScenario.ExecuteAsync(
        new AddApplicationScenarioContext
        {
          Payload = input,
          CorrelationId = Guid.NewGuid()
        });
    initialResult.IsSome.Should().BeTrue();
    ApplicationInput updateItem = new();
    initialResult.IfSome(e =>
    {
      updateItem.Id = e.Id + 10;
      updateItem.Name = $"updated {e.Name}";
      updateItem.Description = $"updated {e.Description}";
    });
    Option<ApplicationResult> updateResult = await UpdateApplicationScenario.ExecuteAsync(
        new UpdateApplicationScenarioContext
        {
          CorrelationId = Guid.NewGuid(),
          Payload = updateItem
        });
    updateResult.IsSome.Should().BeFalse();
    updateResult.IsNone.Should().BeTrue();
  }
}