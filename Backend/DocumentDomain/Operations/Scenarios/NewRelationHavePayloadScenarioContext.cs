namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;

using Common.Scenario;
using EncyclopediaGalactica.Common.Contracts;

public class NewRelationHavePayloadScenarioContext : IHavePayloadScenarioContext<RelationInput>
{
    public RelationInput Payload { get; set; }
    public Guid CorrelationId { get; set; }
}