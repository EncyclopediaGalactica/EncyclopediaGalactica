namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.DocumentType;

using Common.Scenario;
using EncyclopediaGalactica.Common.Contracts;

public class UpdateDocumentTypeScenarioContext : IHavePayloadScenarioContext<DocumentTypeInput>
{
    public DocumentTypeInput? Payload { get; set; }
    public Guid CorrelationId { get; set; }
}