namespace EncyclopediaGalactica.Services.Document.Graphql.Types.Mutations;

using HotChocolate.Types;
using Input;
using Output;
using Resolvers.Document;
using RootTypes;

public class AddDocumentMutation : ObjectTypeExtension<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        descriptor
            .Field("addDocument")
            .Type<NonNullType<DocumentOutput>>()
            .Description("Recording new documents in the system")
            .Argument("newDocument", arg => arg.Type<NonNullType<DocumentInputType>>())
            .ResolveWith<DocumentResolvers>(documentResolvers => documentResolvers.AddAsync(default, default));
    }
}