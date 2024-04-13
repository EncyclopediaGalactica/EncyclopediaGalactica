namespace EncyclopediaGalactica.BusinessLogic.Commands.Relation;

using Contracts;

public interface IGetRelationsCommand
{
    Task<List<RelationResult>> GetAllAsync(CancellationToken cancellationToken = default);
}