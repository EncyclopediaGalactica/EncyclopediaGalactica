namespace DocumentDomain.Spec.Operations.Scenario.Application;

using EncyclopediaGalactica.Common.Contracts;
using EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Application;
using FluentAssertions;
using LanguageExt;

public class DeleteApplicationScenarioShould : ScenarioBaseTest
{
    [Fact]
    public async Task Delete_WhenInputIsValid()
    {
        Dictionary<long, ApplicationResult> store = await SeedApplications(1);

        await DeleteApplicationScenario.ExecuteAsync(
            new DeleteApplicationScenarioContext
            {
                CorrelationId = Guid.NewGuid(),
                Payload = new ApplicationInput
                {
                    Id = store.First().Key
                }
            });
        Option<List<ApplicationResult>> result = await GetApplicationsScenario.ExecuteAsync(
            new GetApplicationsScenarioContext
            {
                CorrelationId = Guid.NewGuid()
            });
        result.IsSome.Should().BeTrue();
        result.IfSome(r => { r.Count.Should().Be(0); });
    }
}