namespace EncyclopediaGalactica.Services.Document.Graphql.Tests.E2E.Tools.Base;

using System.Collections.ObjectModel;
using Bogus;
using Dtos;
using FluentAssertions;

public partial class GraphQLTestBase
{
    protected async Task<List<long>> CreateDocumentDataSetWithMandatoryFieldsOnly(long amount)
    {
        List<long> resultIds = new List<long>();
        for (int i = 0; i < amount; i++)
        {
            DocumentDto data = new Faker<DocumentDto>()
                .RuleFor(dto => dto.Name, faker => faker.Name.FirstName())
                .RuleFor(dto => dto.Description, faker => faker.Name.FullName())
                .Generate();

            string mutationString = """
                                                mutation asd($input: DocumentDtoInput!) {
                                                    addDocument(newDocument: $input) { id name description }
                                                }
                                    """;
            Dictionary<string, object?> payload = new Dictionary<string, object?>
            {
                { nameof(DocumentDto.Id).ToLower(), data.Id },
                { nameof(DocumentDto.Name).ToLower(), data.Name },
                { nameof(DocumentDto.Description).ToLower(), data.Description }
            };

            string result = await ExecuteRequestAsync(query =>
            {
                query.SetQuery(mutationString);
                query.AddVariableValue("input", new ReadOnlyDictionary<string, object?>(payload));
            });
            DocumentDto r = new OperationResultBuilder
            {
                Path = "addDocument",
                QueryResultString = result
            }.Build<DocumentDto>();
            r.Id.Should().BeGreaterOrEqualTo(1);
            resultIds.Add(r.Id);
        }

        return resultIds;
    }
}