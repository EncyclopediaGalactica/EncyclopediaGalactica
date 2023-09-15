namespace Document.Graphql.Types.Document;

using Arguments;
using HotChocolate.Types;
using Resolvers.Document;

public class DeleteDocumentMutation : ObjectTypeExtension<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        descriptor
            .Field("deleteDocument")
            .Type<NonNullType<DocumentDtoType>>()
            .Description("Delete the designated Document.")
            .Argument(ArgumentNames.Document.DocumentId, arg => arg.Type<NonNullType<FloatType>>())
            .ResolveWith<DocumentResolvers>(res => res.DeleteDocumentAsync(default, default));
    }
}