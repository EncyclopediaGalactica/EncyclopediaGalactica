namespace EncyclopediaGalactica.Services.Document.Graphql.Tests.E2E.Tools.Base;

using System.Collections.ObjectModel;
using Bogus;
using Contracts.Input;
using FluentAssertions;

public partial class GraphQLTestBase
{
    protected async Task<List<long>> CreateDocumentDataSetWithMandatoryFieldsOnly(long amount)
    {
        List<long> resultIds = new List<long>();
        for (int i = 0; i < amount; i++)
        {
            DocumentInput data = new Faker<DocumentInput>()
                .RuleFor(dto => dto.Name, faker => faker.Name.FirstName())
                .RuleFor(dto => dto.Description, faker => faker.Name.FullName())
                .Generate();

            string mutationString = """
                                                mutation asd($input: DocumentInput!) {
                                                    addDocument(newDocument: $input) { id name description }
                                                }
                                    """;
            Dictionary<string, object?> payload = new Dictionary<string, object?>
            {
                { nameof(DocumentInput.Id).ToLower(), data.Id },
                { nameof(DocumentInput.Name).ToLower(), data.Name },
                { nameof(DocumentInput.Description).ToLower(), data.Description }
            };

            string result = await ExecuteRequestAsync(query =>
            {
                query.SetQuery(mutationString);
                query.AddVariableValue("input", new ReadOnlyDictionary<string, object?>(payload));
            });
            DocumentInput r = new OperationResultBuilder
            {
                Path = "addDocument",
                QueryResultString = result
            }.Build<DocumentInput>();
            r.Id.Should().BeGreaterOrEqualTo(1);
            resultIds.Add(r.Id);
        }

        return resultIds;
    }
}