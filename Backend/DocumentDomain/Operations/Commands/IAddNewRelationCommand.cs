namespace EncyclopediaGalactica.DocumentDomain.Operations.Commands;

using EncyclopediaGalactica.Common.Contracts;

public interface IAddNewRelationCommand
{
    Task<long> AddNewRelationAsync(RelationInput payload, CancellationToken cancellationToken);
}