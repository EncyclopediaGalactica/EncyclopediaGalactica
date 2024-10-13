namespace EncyclopediaGalactica.DocumentDomain.Operations.Scenarios.RelationType;

using System.Collections.Immutable;
using EncyclopediaGalactica.Common.Contracts;
using Entity;

public static class RelationTypeMappers
{
    public static RelationType MapToRelationType(this RelationTypeInput input) =>
        new()
        {
            Id = input.Id,
            Name = input.Name,
            Description = input.Description,
        };

    public static RelationTypeResult MapToRelationTypeResult(this RelationType relationType) =>
        new()
        {
            Id = relationType.Id,
            Name = relationType.Name,
            Description = relationType.Description,
        };

    public static ImmutableList<RelationTypeResult> MapToRelationTypeResultsImmutableList(this List<RelationType> relationTypes)
    {
        if (relationTypes.Count == 0)
        {
            return ImmutableList<RelationTypeResult>.Empty;
        }

        return relationTypes.Select(item => new RelationTypeResult
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
        }).ToImmutableList();
    }
}