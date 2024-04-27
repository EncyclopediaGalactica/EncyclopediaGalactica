using EncyclopediaGalactica.BusinessLogic.Contracts;
using HotChocolate.Types;

namespace EncyclopediaGalactica.Infrastructure.Graphql.Types.Result;

public class RelationOutput : ObjectType<RelationResult>
{
    protected override void Configure(IObjectTypeDescriptor<RelationResult> descriptor)
    {
        descriptor
            .Field(f => f.Id)
            .Description("Unique identifier of the entity")
            .Type<NonNullType<FloatType>>();

    }
}
