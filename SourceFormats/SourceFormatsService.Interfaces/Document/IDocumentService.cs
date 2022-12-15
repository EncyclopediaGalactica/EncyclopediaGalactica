namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Interfaces.Document;

using Dtos;
using Entities;

/// <summary>
/// IDocument Service interface.
/// </summary>
public interface IDocumentService
{
    Task<DocumentDto> Add(DocumentDto dto);
    Task<List<Document>> GetAll();
    Task<Document> GetById(long id);
}