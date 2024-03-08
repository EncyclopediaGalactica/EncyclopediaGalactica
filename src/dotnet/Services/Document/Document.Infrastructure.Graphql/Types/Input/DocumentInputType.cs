namespace EncyclopediaGalactica.Services.Document.Graphql.Arguments.Types.Input;

using Contracts.Input;
using HotChocolate.Types;

public class DocumentInputType : InputObjectType<DocumentInput>
{
    protected override void Configure(IInputObjectTypeDescriptor<DocumentInput> descriptor)
    {
        descriptor
            .Field(f => f.Id)
            .Description("Unique identifier of the entity")
            .Type<NonNullType<LongType>>();

        descriptor
            .Field(f => f.Name)
            .Description("Name of the entity")
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(f => f.Description)
            .Description("Description of the entity")
            .Type<StringType>();

        descriptor
            .Field(f => f.Uri)
            .Description("The source url of the document")
            .Type<StringType>();

        descriptor
            .Field(f => f.RootStructureNode)
            .Description("The root Structure Node.")
            .Type<StructureNodeInputType>();
    }
}