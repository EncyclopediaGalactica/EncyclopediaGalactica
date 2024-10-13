namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;

using Common.Scenario;
using EncyclopediaGalactica.Common.Contracts;

public class AddDocumentHavePayloadScenarioContext : IHavePayloadScenarioContext<DocumentInput>
{
    public DocumentInput Payload { get; set; } = new DocumentInput();
    public Guid CorrelationId { get; set; }
}