namespace Document.Graphql.Types;

using EncyclopediaGalactica.Services.Document.Dtos;

public class DocumentDtoType : ObjectType<DocumentDto>
{
    protected override void Configure(IObjectTypeDescriptor<DocumentDto> descriptor)
    {
        descriptor
            .Field(f => f.Id)
            .Description("Unique identifier of the entity")
            .Type<IdType>();

        descriptor
            .Field(f => f.Name)
            .Description("Name of the entity")
            .Type<StringType>();

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