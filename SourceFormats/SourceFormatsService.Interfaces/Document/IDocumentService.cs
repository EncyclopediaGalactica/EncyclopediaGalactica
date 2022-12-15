namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Interfaces.Document;

using Dtos;
using Entities;

/// <summary>
///     IDocument Service interface.
///     <remarks>
///         The service provides Api methods to access <see cref="Document" /> objects stored in the system.
///     </remarks>
/// </summary>
public interface IDocumentService
{
    /// <summary>
    ///     Adds a <see cref="Document" /> object to the system with the values represented by the provided
    ///     <see cref="DocumentDto" />.
    /// </summary>
    /// <param name="dto">The input object</param>
    /// <returns>
    ///     Returns a <see cref="Task{TResult}" /> object representing the result of an asynchronous operation.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///     When input is null.
    /// </exception>
    Task<DocumentDto> AddAsync(DocumentDto dto);

    Task<List<Document>> GetAll();
    Task<Document> GetById(long id);
}