using EncyclopediaGalactica.BusinessLogic.Contracts;
using EncyclopediaGalactica.Infrastructure.Graphql.Resolvers.QueryResolvers;
using EncyclopediaGalactica.Infrastructure.Graphql.Types.Result;
using HotChocolate.Types;

namespace EncyclopediaGalactica.Infrastructure.Graphql.Types.Queries;

public class GetRelation : ObjectTypeExtension<Query>
{
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor
            .Field("getRelation")
            .Description(
                $"Returns the defined {nameof(RelationResult)} relation if input provided. " +
                $"If no input is provided it returns the list of {nameof(RelationResult)}s."
            )
            .Type<ListType<RelationOutput>>()
            .ResolveWith<GetRelationQueryResolver>(res => res.GetRelationAsync(default, default, default));
    }
}
