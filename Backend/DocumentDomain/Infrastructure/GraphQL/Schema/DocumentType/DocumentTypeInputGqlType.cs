namespace EncyclopediaGalactica.DocumentDomain.Infrastructure.GraphQL.Schema.DocumentType;

using EncyclopediaGalactica.Common.Contracts;

public class DocumentTypeInputGqlType : InputObjectType<DocumentTypeInput>
{
    protected override void Configure(IInputObjectTypeDescriptor<DocumentTypeInput> descriptor)
    {
        descriptor
            .Field(f => f.Id)
            .Type<LongType>();
        descriptor
            .Field(f => f.Name)
            .Type<StringType>();
        descriptor
            .Field(f => f.Description)
            .Type<StringType>();
    }
}