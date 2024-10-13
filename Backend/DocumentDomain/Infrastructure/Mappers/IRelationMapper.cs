namespace EncyclopediaGalactica.DocumentDomain.Infrastructure.Mappers;

using EncyclopediaGalactica.Common.Contracts;
using Entity;

public interface IRelationMapper
{
    Relation MapRelationInputToRelation(RelationInput payload);
    RelationResult MapRelationToRelationResult(Relation result);
    List<RelationResult> MapRelationsToRelationResults(List<Relation> result);
}