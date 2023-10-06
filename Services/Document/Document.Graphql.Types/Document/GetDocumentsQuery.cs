namespace EncyclopediaGalactica.Services.Document.Graphql.Types.Document;

using HotChocolate.Types;
using Resolvers.Document;

public class GetDocumentsQuery : ObjectTypeExtension<Query>
{
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor
            .Field("getDocuments")
            .Description("Document entity")
            .Type<ListType<DocumentDtoType>>()
            .ResolveWith<DocumentResolvers>(res => res.GetAllAsync(default, default));
    }
}