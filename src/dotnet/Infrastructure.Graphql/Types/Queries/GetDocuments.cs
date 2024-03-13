namespace EncyclopediaGalactica.Infrastructure.Graphql.Types.Queries;

using BusinessLogic.Entities;
using HotChocolate.Types;
using Resolvers.QueryResolvers;
using Result;

public class GetDocuments : ObjectTypeExtension<Query>
{
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor
            .Field("getDocuments")
            .Description($"Returns the list of {nameof(Document)} entities available in the system.")
            .Type<ListType<DocumentOutput>>()
            .ResolveWith<DocumentQueryResolvers>(res => res.GetAllAsync(default, default));
    }
}