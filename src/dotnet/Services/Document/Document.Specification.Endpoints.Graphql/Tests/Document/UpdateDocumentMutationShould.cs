namespace EncyclopediaGalactica.Services.Document.Graphql.Tests.E2E.Tests.Document;

using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Arguments;
using Arguments.Arguments;
using Contracts.Input;
using Errors;
using FluentAssertions;
using Services.Document.Tests.Datasets.DocumentDto;
using Tools.Base;
using Xunit.Abstractions;

[ExcludeFromCodeCoverage]
public class UpdateDocumentMutationShould : GraphQLTestBase
{
    public UpdateDocumentMutationShould(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Theory]
    [ClassData(typeof(UpdateDocumentDto_InputValidation_InvalidDataset))]
    public async Task InputValidation_InvalidValues(DocumentInput input)
    {
        // Arrange
        List<long> documentId = await CreateDocumentDataSetWithMandatoryFieldsOnly(1);
        // dto.Id = documentId[0];

        Dictionary<string, object?> values = new Dictionary<string, object?>();
        foreach (PropertyInfo propertyInfo in input.GetType().GetProperties())
        {
            string propName = $"{char.ToLower(propertyInfo.Name.ToCharArray()[0])}{propertyInfo.Name.Substring(1)}";
            values.Add(propName, propertyInfo.GetValue(input));
        }

        // Act
        string result = await ExecuteRequestAsync(q =>
        {
            q.SetQuery("""
                       mutation asd($documentId: Long!, $updatedDocument: DocumentInput!) {
                           updateDocument(documentId: $documentId, updatedDocument: $updatedDocument) {
                               id name description
                           }
                       }
                       """);
            q.AddVariableValue(ArgumentNames.Document.DocumentId, input.Id);
            q.AddVariableValue(ArgumentNames.Document.UpdatedDocument,
                new ReadOnlyDictionary<string, object?>(values));
        }, _testOutputHelper);

        // Assert
        result.ToLower().Should().Contain(Errors.InvalidInput.ToLower());
    }
}