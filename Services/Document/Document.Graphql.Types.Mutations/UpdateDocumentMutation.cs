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
            .Type<NonNullType<DocumentOutput>>()
            .Description("Updates a document of the system")
            .Argument(ArgumentNames.Document.DocumentId, arg => arg.Type<NonNullType<LongType>>())
            .Argument(ArgumentNames.Document.UpdatedDocument, arg => arg.Type<NonNullType<DocumentInputType>>())
            .ResolveWith<DocumentResolvers>(res => res.UpdateDocumentAsync(default, default));
    }
}