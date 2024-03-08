namespace EncyclopediaGalactica.Services.Document.Graphql.Arguments.Types.Mutations;

using Arguments;
using HotChocolate.Types;
using Infrastructure.Graphql.RootTypes;
using Resolvers.MutationResolvers;
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
            .ResolveWith<DocumentMutationResolvers>(res => res.DeleteAsync(default, default));
    }
}