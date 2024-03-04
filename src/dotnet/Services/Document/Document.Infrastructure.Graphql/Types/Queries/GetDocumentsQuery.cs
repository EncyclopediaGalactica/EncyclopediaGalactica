namespace EncyclopediaGalactica.Services.Document.Graphql.Arguments.Types.Queries;

using HotChocolate.Types;
using Resolvers.QueryResolvers;
using Result;

public class GetDocumentsQuery : ObjectTypeExtension<Query>
{
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor
            .Field("getDocuments")
            .Description("Document entity")
            .Type<ListType<DocumentOutput>>()
            .ResolveWith<DocumentQueryResolvers>(res => res.GetAllAsync(default, default));
    }
}