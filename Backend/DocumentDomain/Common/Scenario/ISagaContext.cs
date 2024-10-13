namespace EncyclopediaGalactica.DocumentDomain.Common.Scenario;

public interface ISagaContext
{
    Guid CorrelationId { get; set; }
}