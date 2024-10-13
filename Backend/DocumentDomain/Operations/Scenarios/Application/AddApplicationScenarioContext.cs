namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Application;

using Common.Scenario;
using EncyclopediaGalactica.Common.Contracts;

public class AddApplicationScenarioContext : IHavePayloadScenarioContext<ApplicationInput>
{
    public ApplicationInput? Payload { get; set; }
    public Guid CorrelationId { get; set; }
}