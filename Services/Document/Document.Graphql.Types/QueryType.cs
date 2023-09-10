namespace Document.Graphql.Types;

using HotChocolate.Types;
using Resolvers;

public class QueryType : ObjectType
{
    protected override void Configure(IObjectTypeDescriptor descriptor)
    {
        descriptor
            .Name(OperationTypeNames.Query)
            .Description("Queries to get data from the system.");

        descriptor
            .Field("getDocuments")
            .Description("Document entity")
            .Type<ListType<DocumentDtoType>>()
            .ResolveWith<DocumentResolvers>(res => res.GetAllAsync(default, default));
    }
}