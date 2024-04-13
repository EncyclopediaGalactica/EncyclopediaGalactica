namespace EncyclopediaGalactica.BusinessLogic.Commands.Relation;

public interface IDeleteRelationCommand
{
    Task DeleteAsync(long relationId, CancellationToken cancellationToken);
}