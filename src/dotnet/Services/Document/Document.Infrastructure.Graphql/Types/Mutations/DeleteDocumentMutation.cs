namespace EncyclopediaGalactica.Services.Document.Graphql.Arguments.Types.Mutations;

using EncyclopediaGalactica.Services.Document.Graphql.Arguments.Arguments;
using EncyclopediaGalactica.Services.Document.Graphql.Arguments.Resolvers;
using HotChocolate.Types;
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
            .ResolveWith<DocumentResolvers>(res => res.DeleteAsync(default, default));
    }
}