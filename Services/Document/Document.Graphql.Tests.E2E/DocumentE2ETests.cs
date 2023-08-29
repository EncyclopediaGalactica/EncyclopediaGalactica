namespace Documents.Graphql.Tests.E2E;

using System.Collections.ObjectModel;
using Xunit.Abstractions;

public class DocumentE2ETests : GraphQLTestBase
{
    public DocumentE2ETests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task Mutation()
    {
        // Arrange
        string mutationString = """
                                            mutation asd($input: DocumentDtoInput!) {
                                                addDocument(newDocument: $input) { id name description }
                                            }
                                """;
        Dictionary<string, object?> payload = new Dictionary<string, object?>
        {
            { "name", "name value" },
            { "description", "description value" },
        };

        // Act
        string result = await ExecuteRequestAsync(query =>
        {
            query.SetQuery(mutationString);
            query.AddVariableValue("input", new ReadOnlyDictionary<string, object?>(payload));
        });
        _testOutputHelper.WriteLine(result);
    }

    [Fact]
    public async Task Query()
    {
        // Arrange
        string queryString = """
                                     query {
                                         document { id name description }
                                     }
                             """;

        // Act
        string result = await ExecuteRequestAsync(query => query.SetQuery(queryString));
        _testOutputHelper.WriteLine(result);
    }
}