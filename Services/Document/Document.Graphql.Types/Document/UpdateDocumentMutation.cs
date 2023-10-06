namespace EncyclopediaGalactica.Services.Document.Graphql.Types.Document;

using Arguments;
using HotChocolate.Types;
using Resolvers.Document;

public class UpdateDocumentMutation : ObjectTypeExtension<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        descriptor
            .Field("updateDocument")
            .Type<NonNullType<DocumentDtoType>>()
            .Description("Updates a document of the system")
            .Argument(ArgumentNames.Document.DocumentId, arg => arg.Type<NonNullType<FloatType>>())
            .Argument(ArgumentNames.Document.UpdatedDocument, arg => arg.Type<NonNullType<DocumentDtoInputType>>())
            .ResolveWith<DocumentResolvers>(res => res.UpdateDocumentAsync(default, default));
    }
}