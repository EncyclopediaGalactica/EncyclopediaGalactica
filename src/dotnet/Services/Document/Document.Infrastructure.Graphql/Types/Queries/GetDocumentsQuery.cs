namespace EncyclopediaGalactica.Services.Document.Graphql.Arguments.Types.Queries;

using EncyclopediaGalactica.Services.Document.Graphql.Arguments.Resolvers;
using HotChocolate.Types;
using Result;

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