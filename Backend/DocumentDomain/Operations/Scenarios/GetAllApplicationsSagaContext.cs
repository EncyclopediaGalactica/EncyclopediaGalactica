namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;

using Common.Scenario;

public class GetAllApplicationsSagaContext : ISagaContext
{
    public Guid CorrelationId { get; set; }
}