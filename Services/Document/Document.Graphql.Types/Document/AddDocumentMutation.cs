namespace EncyclopediaGalactica.Services.Document.Graphql.Types.Document;

using HotChocolate.Types;
using Resolvers.Document;

public class AddDocumentMutation : ObjectTypeExtension<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        descriptor
            .Field("addDocument")
            .Type<NonNullType<DocumentDtoType>>()
            .Description("Recording new documents in the system")
            .Argument("newDocument", arg => arg.Type<NonNullType<DocumentDtoInputType>>())
            .ResolveWith<DocumentResolvers>(documentResolvers => documentResolvers.AddAsync(default, default));
    }
}