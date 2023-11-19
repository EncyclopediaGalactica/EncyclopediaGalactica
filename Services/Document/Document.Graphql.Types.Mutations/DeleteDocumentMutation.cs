namespace EncyclopediaGalactica.Services.Document.Graphql.Types.Mutations;

using Arguments;
using HotChocolate.Types;
using Output;
using Resolvers.Document;
using RootTypes;

public class DeleteDocumentMutation : ObjectTypeExtension<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        descriptor
            .Field("deleteDocument")
            .Type<NonNullType<DocumentGraphqlOutput>>()
            .Description("Delete the designated Document.")
            .Argument(ArgumentNames.Document.DocumentId, arg => arg.Type<NonNullType<FloatType>>())
            .ResolveWith<DocumentResolvers>(res => res.DeleteDocumentAsync(default, default));
    }
}