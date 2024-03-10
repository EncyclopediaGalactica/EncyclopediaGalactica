namespace EncyclopediaGalactica.Infrastructure.Graphql.Types.Mutations;

using HotChocolate.Types;
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
            .Description($"Creates a {nameof(Services.Document)} entity in the system. " +
                         $"If the input contains {nameof(StructureNode)}s those also will be recorded.")
            .Argument("newDocument", arg => arg.Type<NonNullType<DocumentInputType>>())
            .ResolveWith<DocumentMutationResolvers>(documentResolvers => documentResolvers.AddAsync(default, default));
    }
}