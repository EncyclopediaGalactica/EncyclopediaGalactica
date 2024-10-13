namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.DocumentType;

using Common.Scenario;

public class GetDocumentTypesScenarioContext : ISagaContext
{
    public Guid CorrelationId { get; set; }
}