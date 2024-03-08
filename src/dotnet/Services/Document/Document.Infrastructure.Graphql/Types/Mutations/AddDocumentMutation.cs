namespace EncyclopediaGalactica.Services.Document.Graphql.Arguments.Types.Mutations;

using HotChocolate.Types;
using Infrastructure.Graphql.RootTypes;
using Input;
using Resolvers.MutationResolvers;
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
            .ResolveWith<DocumentMutationResolvers>(documentResolvers => documentResolvers.AddAsync(default, default));
    }
}