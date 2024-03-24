namespace EncyclopediaGalactica.BusinessLogic.Commands.Relation;

using Contracts;

public interface IAddNewRelationCommand
{
    Task<long> AddNewRelationAsync(RelationInput payload, CancellationToken cancellationToken);
}