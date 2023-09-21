namespace EncyclopediaGalactica.Services.Document.Graphql.Tests.E2E.Tests.Document;

using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Dtos;
using Errors;
using FluentAssertions;
using Services.Document.Tests.Datasets.Document;
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

        // Assert
        r.Should().BeOfType<DocumentDto>();
        r.Id.Should().BeGreaterThan(0);
    }
}