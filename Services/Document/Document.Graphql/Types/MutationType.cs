namespace Document.Graphql.Types;

using Resolvers;

public class MutationType : ObjectType
{
    protected override void Configure(IObjectTypeDescriptor descriptor)
    {
        descriptor
            .Name(OperationTypeNames.Mutation)
            .Description("Mutation types to record data in the system");

        descriptor
            .Field("addDocument")
            .Type<NonNullType<DocumentDtoType>>()
            .Description("Recording new documents in the system")
            .Argument("newDocument", arg => arg.Type<NonNullType<DocumentDtoInputType>>())
            .ResolveWith<DocumentResolvers>(documentResolvers => documentResolvers.AddAsync(default, default));
    }
}