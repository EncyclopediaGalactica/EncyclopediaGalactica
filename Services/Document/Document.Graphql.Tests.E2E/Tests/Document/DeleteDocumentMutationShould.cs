using ArgumentNames = Document.Graphql.Arguments.ArgumentNames;
using Errors = Document.Errors.Errors;

namespace Documents.Graphql.Tests.E2E.Tests.Document;

using FluentAssertions;
using Tools.Base;
using Xunit.Abstractions;

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
                       mutation delete($documentId: Float!) {
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
                       mutation delete($documentId: Float!) {
                           deleteDocument(documentId: $documentId) { id }
                       }
                       """);
            q.AddVariableValue(ArgumentNames.Document.DocumentId, 100);
        }, _testOutputHelper);

        // Assert
        result.ToLower().Should().Contain(Errors.NoSuchItem.ToLower());
    }
}