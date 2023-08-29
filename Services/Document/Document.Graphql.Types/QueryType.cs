namespace Document.Graphql.Types;

using EncyclopediaGalactica.Services.Document.Dtos;
using HotChocolate.Types;

public class QueryType : ObjectType
{
    protected override void Configure(IObjectTypeDescriptor descriptor)
    {
        descriptor
            .Name(OperationTypeNames.Query)
            .Description("Queries to get data from the system.");

        descriptor
            .Field("document")
            .Description("Document entity")
            .Type<ListType<DocumentDtoType>>()
            .Resolve(r =>
            {
                return new List<DocumentDto>
                {
                    new DocumentDto { Id = 1, Name = "one", Description = "one d" },
                    new DocumentDto { Id = 2, Name = "two", Description = "two d" },
                };
            });
    }
}