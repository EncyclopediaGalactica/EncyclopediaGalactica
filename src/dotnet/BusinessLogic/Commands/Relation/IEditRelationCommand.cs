namespace EncyclopediaGalactica.BusinessLogic.Commands.Relation;

using Contracts;

public interface IEditRelationCommand
{
    Task EditAsync(RelationInput relationInput, CancellationToken cancellationToken = default);
}