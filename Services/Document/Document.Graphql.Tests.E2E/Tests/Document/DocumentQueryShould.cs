namespace Documents.Graphql.Tests.E2E.Tests.Document;

using Tools.Base;
using Xunit.Abstractions;

public class DocumentQueryShould : GraphQLTestBase
{
    public DocumentQueryShould(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
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