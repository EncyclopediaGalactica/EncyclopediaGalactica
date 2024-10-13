namespace EncyclopediaGalactica.DocumentDomain.Operations.Commands;

using EncyclopediaGalactica.Common.Contracts;

public interface IEditRelationCommand
{
    Task EditAsync(RelationInput relationInput, CancellationToken cancellationToken = default);
}