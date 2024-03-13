namespace EncyclopediaGalactica.Infrastructure.Graphql.Types.Mutations;

using Arguments;
using HotChocolate.Types;
using Resolvers.MutationResolvers;
using Result;

public class DeleteDocument : ObjectTypeExtension<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        descriptor
            .Field("deleteDocument")
            .Type<DocumentOutput>()
            .Description("Deletes the designated Document. The delete operation also includes cleaning up the " +
                         "accompanied structures like Structure tree.")
            .Argument(ArgumentNames.Document.DocumentId, arg => arg.Type<NonNullType<LongType>>())
            .ResolveWith<DocumentMutationResolvers>(res => res.DeleteAsync(default, default, default));
    }
}