namespace Document.Graphql.Types;

using EncyclopediaGalactica.Services.Document.Dtos;
using HotChocolate.Types;

public class DocumentDtoType : ObjectType<DocumentDto>
{
    protected override void Configure(IObjectTypeDescriptor<DocumentDto> descriptor)
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
    }
}