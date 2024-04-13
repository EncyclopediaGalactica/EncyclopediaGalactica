namespace EncyclopediaGalactica.BusinessLogic.Mappers;

using Contracts;
using Entities;

/// <inheritdoc />
public class DocumentMapper : IDocumentMapper
{
    /// <inheritdoc />
    public Document MapDocumentInputToDocument(DocumentInput input)
    {
        return new Document
        {
            Id = input.Id,
            Name = input.Name,
            Description = input.Description,
            Uri = input?.Uri
        };
    }

    /// <inheritdoc />
    public List<DocumentResult> MapDocumentsToDocumentResults(List<Document> l)
    {
        List<DocumentResult> resultList = new List<DocumentResult>();
        if (!l.Any()) return resultList;

        foreach (Document item in l)
        {
            resultList.Add(MapDocumentToDocumentResult(item));
        }

        return resultList;
    }

    /// <inheritdoc />
    public DocumentResult MapDocumentToDocumentResult(Document document)
    {
        return new DocumentResult
        {
            Id = document.Id,
            Name = document.Name,
            Description = document.Description,
            Uri = document?.Uri
        };
    }
}