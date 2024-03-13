namespace EncyclopediaGalactica.Infrastructure.Graphql.Types.Result;

using BusinessLogic.Contracts;
using HotChocolate.Types;
using Resolvers.FieldResolvers;

public class DocumentOutput : ObjectType<DocumentResult>
{
    protected override void Configure(IObjectTypeDescriptor<DocumentResult> descriptor)
    {
        descriptor
            .Field(f => f.Id)
            .Description("Unique identifier of the entity")
            .Type<NonNullType<FloatType>>();

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
            .Field(f => f.StructureNode)
            .Description("The root Structure Node of the Document")
            .Type<StructureNodeOutput>()
            .ResolveWith<StructureNodeFieldResolvers>(r => r.GetNode(default, default));
    }
}