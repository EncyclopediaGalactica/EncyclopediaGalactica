namespace EncyclopediaGalactica.Infrastructure.Graphql.Types.Queries;

using BusinessLogic.Entities;
using HotChocolate.Types;
using Resolvers.QueryResolvers;
using Result;

public class GetDocumentById : ObjectTypeExtension<Query>
{
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor
            .Field("getDocumentById")
            .Description($"Returns the designated {nameof(Document)} entity.")
            .Type<NonNullType<DocumentOutput>>()
            .Argument(Arguments.ArgumentNames.Document.DocumentId, arg => arg.Type<NonNullType<LongType>>())
            .ResolveWith<DocumentQueryResolvers>(res => res.GetByIdAsync(default, default, default));
    }
}