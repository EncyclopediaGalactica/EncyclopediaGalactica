namespace EncyclopediaGalactica.DocumentDomain.Infrastructure.GraphQL.Schema.DocumentType;

using Resolvers;
using Types;

public class DocumentTypeMutation : ObjectTypeExtension<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        descriptor
            .Field("addDocumentType")
            .Description("Creating a new Document Type entity in the system.")
            .Argument("input", input => input.Type<DocumentTypeInputGqlType>())
            .Type<DocumentTypeResultGqlType>()
            .ResolveWith<DocumentTypeResolver>(resolver => resolver.AddDocumentType(default, default, default));
        // descriptor
        // .Field("modifyDocumentType")
        // .Argument("input", input => input.Type<DocumentTypeInputGqlType>())
        // .Type<DocumentTypeResultGqlType>();
        // descriptor
        // .Field("deleteDocumentType")
        // .Argument("input", input => input.Type<DocumentTypeInputGqlType>())
        // .Type<DocumentTypeResultGqlType>();
    }
}