namespace EncyclopediaGalactica.Services.Document.Mappers.Document;

using Dtos;
using Entities;
using Interfaces;

/// <inheritdoc />
public class DocumentMappers : IDocumentMappers
{
    /// <inheritdoc />
    public Document MapDocumentDtoToDocument(DocumentDto dto)
    {
        return new Document
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            Uri = dto?.Uri
        };
    }

    /// <inheritdoc />
    public DocumentDto MapDocumentToDocumentDto(Document document)
    {
        return new DocumentDto
        {
            Id = document.Id,
            Name = document.Name,
            Description = document.Description,
            Uri = document?.Uri
        };
    }

    /// <inheritdoc />
    public List<DocumentDto> MapDocumentsToDocumentDtos(List<Document> l)
    {
        List<DocumentDto> resultList = new List<DocumentDto>();
        if (!l.Any()) return resultList;
        foreach (Document item in l)
        {
            resultList.Add(MapDocumentToDocumentDto(item));
        }

        return resultList;
    }
}