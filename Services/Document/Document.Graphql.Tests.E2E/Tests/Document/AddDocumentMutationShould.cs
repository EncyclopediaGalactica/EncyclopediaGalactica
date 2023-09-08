using Errors = Document.Errors.Errors;

namespace Documents.Graphql.Tests.E2E.Tests.Document;

using System.Collections.ObjectModel;
using EncyclopediaGalactica.Services.Document.Dtos;
using EncyclopediaGalactica.Services.Document.Tests.Datasets.Document;
using FluentAssertions;
using Tools;
using Tools.Base;
using Xunit.Abstractions;

public class AddDocumentMutationShould : GraphQLTestBase
{
    public AddDocumentMutationShould(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Theory]
    [ClassData(typeof(AddDocumentInputValidationInvalidDataset))]
    public async Task InputValidation_InvalidInput(DocumentDto input)
    {
        // Arrange
        string mutationString = """
                                            mutation asd($input: DocumentDtoInput!) {
                                                addDocument(newDocument: $input) { id name description }
                                            }
                                """;
        Dictionary<string, object?> payload = new Dictionary<string, object?>
        {
            { nameof(DocumentDto.Id).ToLower(), input.Id },
            { nameof(DocumentDto.Name).ToLower(), input.Name },
            { nameof(DocumentDto.Description).ToLower(), input.Description }
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
    [ClassData(typeof(AddDocumentValidDataForMandatoryFields))]
    public async Task MandatoryFields(DocumentDto input)
    {
        // Arrange
        string mutationString = """
                                mutation asd($input: DocumentDtoInput!) {
                                    addDocument(newDocument: $input) { id name description }
                                }
                                """;
        Dictionary<string, object?> payload = new Dictionary<string, object?>
        {
            { nameof(DocumentDto.Id).ToLower(), input.Id },
            { nameof(DocumentDto.Name).ToLower(), input.Name },
            { nameof(DocumentDto.Description).ToLower(), input.Description },
        };

        // Act
        string result = await ExecuteRequestAsync(query =>
        {
            query.SetQuery(mutationString);
            query.AddVariableValue("input", new ReadOnlyDictionary<string, object?>(payload));
        }, _testOutputHelper);
        DocumentDto r = new OperationResultBuilder
        {
            Path = "addDocument",
            QueryResultString = result
        }.Build<DocumentDto>();
        r.Should().BeOfType<DocumentDto>();
        r.Id.Should().BeGreaterThan(0);
    }
}