namespace EncyclopediaGalactica.DocumentDomain.Operations.Commands;

using EncyclopediaGalactica.Common.Contracts;

public interface IGetRelationsCommand
{
    Task<List<RelationResult>> GetAllAsync(CancellationToken cancellationToken = default);
}