namespace EncyclopediaGalactica.Services.Document.Mappers.Document;

using Contracts.Input;
using Entities;
using Interfaces;

/// <inheritdoc />
public class DocumentMappers : IDocumentMappers
{
    /// <inheritdoc />
    public Document MapDocumentDtoToDocument(DocumentGraphqlInput graphqlInput)
    {
        return new Document
        {
            Id = graphqlInput.Id,
            Name = graphqlInput.Name,
            Description = graphqlInput.Description,
            Uri = graphqlInput?.Uri
        };
    }

    /// <inheritdoc />
    public DocumentGraphqlInput MapDocumentToDocumentDto(Document document)
    {
        return new DocumentGraphqlInput
        {
            Id = document.Id,
            Name = document.Name,
            Description = document.Description,
            Uri = document?.Uri
        };
    }

    /// <inheritdoc />
    public List<DocumentGraphqlInput> MapDocumentsToDocumentDtos(List<Document> l)
    {
        List<DocumentGraphqlInput> resultList = new List<DocumentGraphqlInput>();
        if (!l.Any()) return resultList;
        foreach (Document item in l)
        {
            resultList.Add(MapDocumentToDocumentDto(item));
        }

        return resultList;
    }
}