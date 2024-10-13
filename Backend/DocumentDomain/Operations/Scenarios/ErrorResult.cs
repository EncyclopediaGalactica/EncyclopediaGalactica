namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios;

public record ErrorResult(Guid CorrelationId, string ErrorMessage);