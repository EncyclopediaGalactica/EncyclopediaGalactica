namespace EncyclopediaGalactica.Services.Document.Graphql.Tests.E2E.Tests.Document;

using System.Diagnostics.CodeAnalysis;
using Arguments;
using Arguments.Arguments;
using Contracts.Input;
using Errors;
using FluentAssertions;
using Tools;
using Tools.Base;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
public class DeleteDocumentMutationShould : GraphQLTestBase
{
    public DeleteDocumentMutationShould(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task InputValidation_InvalidInput()
    {
        // Arrange && Act
        string result = await ExecuteRequestAsync(q =>
        {
            q.SetQuery("""
                       mutation delete($documentId: Long!) {
                           deleteDocument(documentId: $documentId) { id }
                       }
                       """);
            q.AddVariableValue(ArgumentNames.Document.DocumentId, 0);
        }, _testOutputHelper);

        // Assert
        result.ToLower().Should().Contain(Errors.InvalidInput.ToLower());
    }

    [Fact]
    public async Task NoSuchItem()
    {
        // Arrange && Act
        string result = await ExecuteRequestAsync(q =>
        {
            q.SetQuery("""
                       mutation delete($documentId: Long!) {
                           deleteDocument(documentId: $documentId) { id }
                       }
                       """);
            q.AddVariableValue(ArgumentNames.Document.DocumentId, 100);
        }, _testOutputHelper);

        // Assert
        result.ToLower().Should().Contain(Errors.NoSuchItem.ToLower());
    }

    [Fact]
    public async Task Delete()
    {
        // Arrange
        List<long> recorded = await CreateDocumentDataSetWithMandatoryFieldsOnly(1);

        // Act
        string deleteResult = await ExecuteRequestAsync(q =>
        {
            q.SetQuery("""
                       mutation delete($documentId: Long!) {
                           deleteDocument(documentId: $documentId) { id }
                       }
                       """);
            q.AddVariableValue(ArgumentNames.Document.DocumentId, recorded.First());
        }, _testOutputHelper);

        // Assert
        string result = await ExecuteRequestAsync(q =>
        {
            q.SetQuery(
                """
                query {
                    getDocuments { id name description }
                }
                """);
        }, _testOutputHelper);
        List<DocumentInput> resultList = new OperationResultBuilder()
        {
            Path = "getDocuments",
            QueryResultString = result
        }.Build<List<DocumentInput>>();

        resultList.Count.Should().Be(0);
    }
}