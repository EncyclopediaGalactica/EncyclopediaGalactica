namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;

using Common.Scenario;

public class GetRelationByIdHavePayloadScenarioContext : IHavePayloadScenarioContext<long>
{
    public long Payload { get; set; }
    public Guid CorrelationId { get; set; }
}