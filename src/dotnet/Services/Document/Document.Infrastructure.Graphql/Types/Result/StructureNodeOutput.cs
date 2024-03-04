namespace EncyclopediaGalactica.Services.Document.Graphql.Arguments.Types.Result;

using Contracts.Output;
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