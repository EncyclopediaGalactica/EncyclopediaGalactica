namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;

using Common.Scenario;
using EncyclopediaGalactica.Common.Contracts;

public class UpdateDocumentHavePayloadScenarioContext : IHavePayloadScenarioContext<DocumentInput>
{
    public DocumentInput Payload { get; set; }
    public Guid CorrelationId { get; set; }
}