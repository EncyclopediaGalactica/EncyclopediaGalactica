namespace EncyclopediaGalactica.Services.Document.Graphql.Tests.E2E.Tests.Document;

using System.Diagnostics.CodeAnalysis;
using Contracts.Input;
using FluentAssertions;
using Tools;
using Tools.Base;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "DocumentService")]
[Trait("Category", "GraphQL")]
[Trait("Category", "E2E")]
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
        List<DocumentGraphqlInput> result = new OperationResultBuilder()
        {
            Path = "getDocuments",
            QueryResultString = requestResult
        }.Build<List<DocumentGraphqlInput>>();

        // Assert
        result.Should().BeOfType<List<DocumentGraphqlInput>>();
        result.Count.Should().Be(amount);
    }
}