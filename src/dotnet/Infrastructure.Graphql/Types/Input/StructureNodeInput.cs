namespace EncyclopediaGalactica.Infrastructure.Graphql.Types.Input;

using BusinessLogic.Contracts;
using HotChocolate.Types;

public class StructureNodeInputType : InputObjectType<StructureNodeInput>
{
    protected override void Configure(IInputObjectTypeDescriptor<StructureNodeInput> descriptor)
    {
        descriptor
            .Field(f => f.Id)
            .Description("Id field.")
            .Type<LongType>();

        descriptor
            .Field(f => f.DocumentId)
            .Description("Id of the Document entity this structure node with other nodes describes.")
            .Type<LongType>();

        descriptor
            .Field(f => f.IsRootNode)
            .Description("If the node is root node.")
            .Type<IntType>()
            .DefaultValue(0);
    }
}