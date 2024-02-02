namespace EncyclopediaGalactica.Services.Document.Repository.Interfaces;

using Entities;
using Exceptions;
using Microsoft.EntityFrameworkCore;

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

    /// <summary>
    ///     Deletes the designated <see cref="Document" /> entity.
    /// </summary>
    /// <param name="documentId">Unique identifier of the <see cref="Document" /> entity to be deleted.</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <exception cref="DocumentNotFoundException">
    ///     When input is null.
    /// </exception>
    /// <exception cref="DbUpdateException">
    ///     Error happened while saving into the database
    /// </exception>
    /// <exception cref="DbUpdateConcurrencyException">
    ///     Concurrency violation happened while saving into the database.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    ///     When operation is cancelled using <see cref="CancellationToken" />
    /// </exception>
    Task DeleteAsync(long documentId, CancellationToken cancellationToken = default);
}