namespace EncyclopediaGalactica.DocumentDomain.Operations.Commands;

public interface IDeleteRelationCommand
{
    Task DeleteAsync(long relationId, CancellationToken cancellationToken);
}