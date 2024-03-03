namespace EncyclopediaGalactica.Services.Document.Graphql.Arguments.Types.Mutations;

using EncyclopediaGalactica.Services.Document.Graphql.Arguments.Resolvers;
using HotChocolate.Types;
using Input;
using Result;

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