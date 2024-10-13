namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;

using Common.Scenario;

public class GetDocumentByIdContext : IHavePayloadScenarioContext<long>
{
    public long Payload { get; set; }
    public Guid CorrelationId { get; set; }
}