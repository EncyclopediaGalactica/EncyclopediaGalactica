namespace EncyclopediaGalactica.DocumentDomain.Infrastructure.GraphQL.Types;

using EncyclopediaGalactica.Common.Contracts;

public class DocumentTypeResultGqlType : ObjectType<DocumentTypeResult>
{
    protected override void Configure(IObjectTypeDescriptor<DocumentTypeResult> descriptor)
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