namespace EncyclopediaGalactica.Services.Document.Graphql.Types.Mutations;

using Arguments;
using HotChocolate.Types;
using Input;
using Output;
using Resolvers.Document;
using RootTypes;

public class UpdateDocumentMutation : ObjectTypeExtension<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        descriptor
            .Field("updateDocument")
            .Type<NonNullType<DocumentGraphqlOutput>>()
            .Description("Updates a document of the system")
            .Argument(ArgumentNames.Document.DocumentId, arg => arg.Type<NonNullType<FloatType>>())
            .Argument(ArgumentNames.Document.UpdatedDocument, arg => arg.Type<NonNullType<DocumentGraphqlInputType>>())
            .ResolveWith<DocumentResolvers>(res => res.UpdateDocumentAsync(default, default));
    }
}