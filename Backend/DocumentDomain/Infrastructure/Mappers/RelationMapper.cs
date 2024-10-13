namespace EncyclopediaGalactica.DocumentDomain.Infrastructure.Mappers;

using EncyclopediaGalactica.Common.Contracts;
using Entity;

public class RelationMapper : IRelationMapper
{
    public Relation MapRelationInputToRelation(RelationInput payload)
    {
        return new Relation
        {
            Id = payload.Id,
            LeftId = payload.LeftEndId,
            RightId = payload.RightEndId
        };
    }

    public RelationResult MapRelationToRelationResult(Relation result)
    {
        return new RelationResult
        {
            Id = result.Id,
            LeftDocumentId = result.LeftId,
            RightDocumentId = result.RightId
            // LeftDocument = result.,
            // RightDocument = result.RightEndStructureNodeId
        };
    }

    public List<RelationResult> MapRelationsToRelationResults(List<Relation> r)
    {
        List<RelationResult> result = new List<RelationResult>();
        r.ForEach(item => result.Add(MapRelationToRelationResult(item)));
        return result;
    }
}