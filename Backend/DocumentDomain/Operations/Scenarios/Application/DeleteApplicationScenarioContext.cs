namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Application;

using Common.Scenario;
using EncyclopediaGalactica.Common.Contracts;

public class DeleteApplicationScenarioContext : IHavePayloadScenarioContext<ApplicationInput>
{
    public Guid CorrelationId { get; set; }
    public ApplicationInput? Payload { get; set; }
}