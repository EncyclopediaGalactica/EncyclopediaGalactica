namespace EncyclopediaGalactica.Services.Document.Graphql.Tests.E2E.Tests.Document;

using System.Diagnostics.CodeAnalysis;
using Contracts.Input;
using FluentAssertions;
using Tools;
using Tools.Base;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
public class GetDocumentsQueryShould : GraphQLTestBase
{
    public GetDocumentsQueryShould(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GetDocumentsQuery(int amount)
    {
        // Arrange
        await CreateDocumentDataSetWithMandatoryFieldsOnly(amount);
        string queryString = """
                                     query {
                                         getDocuments { id name description }
                                     }
                             """;

        // Act
        string requestResult = await ExecuteRequestAsync(
            query => query.SetQuery(queryString),
            _testOutputHelper);
        List<DocumentInput> result = new OperationResultBuilder()
        {
            Path = "getDocuments",
            QueryResultString = requestResult
        }.Build<List<DocumentInput>>();

        // Assert
        result.Should().BeOfType<List<DocumentInput>>();
        result.Count.Should().Be(amount);
    }
}