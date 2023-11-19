namespace EncyclopediaGalactica.Services.Document.Graphql.Tests.E2E.Tests.Document;

using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Contracts.Input;
using Errors;
using FluentAssertions;
using Services.Document.Tests.Datasets.DocumentDto;
using Tools;
using Tools.Base;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[Trait("Category", "DocumentService")]
[Trait("Category", "GraphQL")]
[Trait("Category", "E2E")]
public class AddDocumentMutationShould : GraphQLTestBase
{
    public AddDocumentMutationShould(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Theory]
    [ClassData(typeof(AddDocumentDtoInputValidation_InvalidDataset))]
    public async Task InputValidation_InvalidInput(DocumentGraphqlInput graphqlInput)
    {
        // Arrange
        string mutationString = """
                                            mutation asd($input: DocumentGraphqlInput!) {
                                                addDocument(newDocument: $input) { id name description }
                                            }
                                """;
        Dictionary<string, object?> payload = new Dictionary<string, object?>
        {
            { nameof(DocumentGraphqlInput.Id).ToLower(), graphqlInput.Id },
            { nameof(DocumentGraphqlInput.Name).ToLower(), graphqlInput.Name },
            { nameof(DocumentGraphqlInput.Description).ToLower(), graphqlInput.Description }
        };

        // Act
        string result = await ExecuteRequestAsync(query =>
        {
            query.SetQuery(mutationString);
            query.AddVariableValue("input", new ReadOnlyDictionary<string, object?>(payload));
        }, _testOutputHelper);
        result.ToLower().Should().Contain(Errors.InvalidInput.ToLower());
    }

    [Theory]
    [ClassData(typeof(AddDocumentDto_Add_ValidDataForMandatoryFields))]
    public async Task MandatoryFields(DocumentGraphqlInput graphqlInput)
    {
        // Arrange
        string mutationString = """
                                mutation asd($input: DocumentGraphqlInput!) {
                                    addDocument(newDocument: $input) { id name description }
                                }
                                """;
        Dictionary<string, object?> payload = new Dictionary<string, object?>
        {
            { nameof(DocumentGraphqlInput.Id).ToLower(), graphqlInput.Id },
            { nameof(DocumentGraphqlInput.Name).ToLower(), graphqlInput.Name },
            { nameof(DocumentGraphqlInput.Description).ToLower(), graphqlInput.Description },
        };

        // Act
        string result = await ExecuteRequestAsync(query =>
        {
            query.SetQuery(mutationString);
            query.AddVariableValue("input", new ReadOnlyDictionary<string, object?>(payload));
        }, _testOutputHelper);
        DocumentGraphqlInput r = new OperationResultBuilder
        {
            Path = "addDocument",
            QueryResultString = result
        }.Build<DocumentGraphqlInput>();

        // Assert
        r.Should().BeOfType<DocumentGraphqlInput>();
        r.Id.Should().BeGreaterThan(0);
    }
}