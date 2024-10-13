namespace EncyclopediaGalactica.DocumentDomain.Operations.Commands;

using EncyclopediaGalactica.Common.Contracts;

public interface IGetRelationByIdCommand
{
    Task<RelationResult> GetByIdAsync(long relationId, CancellationToken cancellationToken);
}