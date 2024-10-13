namespace EncyclopediaGalactica.DocumentDomain.Infrastructure.GraphQL.Schema.DocumentType;

using Resolvers;
using Types;

public class DocumentTypeQueries : ObjectTypeExtension<Query>
{
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor
            .Field("getDocumentTypes")
            .Type<ListType<DocumentTypeResultGqlType>>()
            .ResolveWith<DocumentTypeResolver>(r => r.GetDocumentTypes(default, default));
        descriptor
            .Field("getDocumentTypeById")
            .Type<DocumentTypeResultGqlType>()
            .ResolveWith<DocumentTypeResolver>(r => r.GetDocumentTypeById(default, default));
    }
}