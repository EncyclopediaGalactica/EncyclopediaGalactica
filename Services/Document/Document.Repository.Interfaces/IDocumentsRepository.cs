namespace EncyclopediaGalactica.Services.Document.SourceFormatsRepository.Interfaces;

using Entities;

public interface IDocumentsRepository
{
    Task<Document> AddAsync(Document document, CancellationToken cancellationToken = default);
    Task<Document> GetByIdAsync(long id, CancellationToken cancellationToken = default);

    Task<List<Document>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Updates the details of the designated <see cref="Document" /> entity based on the provided details.
    /// </summary>
    /// <param name="documentId">The designated <see cref="Document" /></param>
    /// <param name="documentWithNewValues">The provided details</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns></returns>
    Task<Document> UpdateAsync(
        long documentId,
        Document documentWithNewValues,
        CancellationToken cancellationToken = default);
}