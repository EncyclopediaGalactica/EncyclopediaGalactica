namespace EncyclopediaGalactica.BusinessLogic.Commands.Relation;

using Contracts;

public interface IGetRelationByIdCommand
{
    Task<RelationResult> GetByIdAsync(long relationId, CancellationToken cancellationToken);
}