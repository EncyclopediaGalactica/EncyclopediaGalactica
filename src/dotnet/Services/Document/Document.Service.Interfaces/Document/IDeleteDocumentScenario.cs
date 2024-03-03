namespace EncyclopediaGalactica.Services.Document.Service.Interfaces.Document;

using Entities;
using Exceptions;

public interface IDeleteDocumentScenario
{
    /// <summary>
    ///     Deletes the designated <see cref="Entities.Document" /> entity.
    /// </summary>
    /// <param name="documentId">The unique identifier of the <see cref="Document" /> entity to be deleted.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <exception cref="InvalidInputToDocumentServiceException">
    ///     Invalid input provided to the service
    /// </exception>
    /// <exception cref="DocumentServiceOperationCancelledException">
    ///     When the operation is cancelled by a <see cref="CancellationToken" />.
    /// </exception>
    /// <exception cref="NoSuchItemDocumentServiceException">
    ///     When there is no such entity in the system based on its unique identifier.
    /// </exception>
    /// <exception cref="UnknownErrorDocumentServiceException">
    ///     In case of any other errors.
    /// </exception>
    Task DeleteAsync(long documentId, CancellationToken cancellationToken = default);
}