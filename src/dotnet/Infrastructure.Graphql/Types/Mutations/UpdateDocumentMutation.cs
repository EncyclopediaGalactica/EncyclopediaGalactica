namespace EncyclopediaGalactica.Infrastructure.Graphql.Types.Mutations;

using Arguments;
using HotChocolate.Types;
using Input;
using Resolvers.MutationResolvers;
using Result;

public class UpdateDocumentMutation : ObjectTypeExtension<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        descriptor
            .Field("updateDocument")
            .Type<NonNullType<DocumentOutput>>()
            .Description("Updates a document of the system")
            .Argument(ArgumentNames.Document.DocumentId, arg => arg.Type<NonNullType<LongType>>())
            .Argument(ArgumentNames.Document.UpdatedDocument, arg => arg.Type<NonNullType<DocumentInputType>>())
            .ResolveWith<DocumentMutationResolvers>(res => res.UpdateDocumentAsync(default, default));
    }
}