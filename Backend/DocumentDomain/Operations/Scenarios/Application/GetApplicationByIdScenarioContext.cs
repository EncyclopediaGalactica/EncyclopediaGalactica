namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.Application;

using EncyclopediaGalactica.Common.Contracts;

public class GetApplicationByIdScenarioContext
{
    public Guid CorrelationId { get; set; }
    public ApplicationInput Payload { get; set; }
}