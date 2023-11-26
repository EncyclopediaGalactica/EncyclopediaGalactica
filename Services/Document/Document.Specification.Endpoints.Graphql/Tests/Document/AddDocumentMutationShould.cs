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
public class AddDocumentMutationShould : GraphQLTestBase
{
    public AddDocumentMutationShould(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Theory]
    [ClassData(typeof(AddDocumentDtoInputValidation_InvalidDataset))]
    public async Task InputValidation_InvalidInput(DocumentInput input)
    {
        // Arrange
        string mutationString = """
                                            mutation asd($input: DocumentInput!) {
                                                addDocument(newDocument: $input) { id name description }
                                            }
                                """;
        Dictionary<string, object?> payload = new Dictionary<string, object?>
        {
            { nameof(DocumentInput.Id).ToLower(), input.Id },
            { nameof(DocumentInput.Name).ToLower(), input.Name },
            { nameof(DocumentInput.Description).ToLower(), input.Description }
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
    public async Task MandatoryFields(DocumentInput input)
    {
        // Arrange
        string mutationString = """
                                mutation asd($input: DocumentInput!) {
                                    addDocument(newDocument: $input) { id name description }
                                }
                                """;
        Dictionary<string, object?> payload = new Dictionary<string, object?>
        {
            { nameof(DocumentInput.Id).ToLower(), input.Id },
            { nameof(DocumentInput.Name).ToLower(), input.Name },
            { nameof(DocumentInput.Description).ToLower(), input.Description },
        };

        // Act
        string result = await ExecuteRequestAsync(query =>
        {
            query.SetQuery(mutationString);
            query.AddVariableValue("input", new ReadOnlyDictionary<string, object?>(payload));
        }, _testOutputHelper);
        DocumentInput r = new OperationResultBuilder
        {
            Path = "addDocument",
            QueryResultString = result
        }.Build<DocumentInput>();

        // Assert
        r.Should().BeOfType<DocumentInput>();
        r.Id.Should().BeGreaterThan(0);
    }
}