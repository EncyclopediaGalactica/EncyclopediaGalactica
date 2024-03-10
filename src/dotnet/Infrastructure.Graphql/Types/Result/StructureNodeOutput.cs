namespace EncyclopediaGalactica.Infrastructure.Graphql.Types.Result;

using HotChocolate.Types;

public class StructureNodeOutput : ObjectType<StructureNodeResult>
{
    protected override void Configure(IObjectTypeDescriptor<StructureNodeResult> descriptor)
    {
        descriptor
            .Field(f => f.Id)
            .Description("Unique identifier of the entity")
            .Type<NonNullType<FloatType>>();
    }
}