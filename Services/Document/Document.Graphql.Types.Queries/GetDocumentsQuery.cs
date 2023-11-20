namespace EncyclopediaGalactica.Services.Document.Graphql.Types.Queries;

using HotChocolate.Types;
using Output;
using Resolvers.Document;
using RootTypes;

public class GetDocumentsQuery : ObjectTypeExtension<Query>
{
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor
            .Field("getDocuments")
            .Description("Document entity")
            .Type<ListType<DocumentOutput>>()
            .ResolveWith<DocumentResolvers>(res => res.GetAllAsync(default, default));
    }
}