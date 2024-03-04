namespace EncyclopediaGalactica.Services.Document.Graphql.Arguments.Types.Mutations;

using Arguments;
using HotChocolate.Types;
using Resolvers.QueryResolvers;
using Result;

public class DeleteDocumentMutation : ObjectTypeExtension<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        descriptor
            .Field("deleteDocument")
            .Type<NonNullType<DocumentOutput>>()
            .Description("Delete the designated Document.")
            .Argument(ArgumentNames.Document.DocumentId, arg => arg.Type<NonNullType<LongType>>())
            .ResolveWith<DocumentQueryResolvers>(res => res.DeleteAsync(default, default));
    }
}