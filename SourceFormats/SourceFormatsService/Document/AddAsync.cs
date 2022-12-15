namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Document;

using Dtos;
using Entities;

public partial class DocumentService
{
    /// <inheritdoc />
    public async Task<DocumentDto> AddAsync(DocumentDto dto)
    {
        _guardsService.NotNull(dto);

        Document document = _mappers.
    }
}