namespace EncyclopediaGalactica.Services.Document.Service.Interfaces.Document;

using Contracts.Input;
using Contracts.Output;
using Entities;
using Exceptions;

/// <summary>
///     IDocument IAM.Service interface.
///     <remarks>
///         The service provides IAM.Api methods to access <see cref="Document" /> objects stored in the system.
///     </remarks>
/// </summary>
public partial interface IDocumentService
{
    /// <summary>
    ///     Adds a <see cref="Document" /> object to the system with the values represented by the provided
    ///     <see cref="DocumentInput" />.
    /// </summary>
    /// <param name="inputInput">The input object</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     Returns a <see cref="Task{TResult}" /> object representing the result of an asynchronous operation.
    /// </returns>
    /// <exception cref="InvalidInputToDocumentServiceException">
    ///     When invalid input is provided to the service
    /// </exception>
    /// <exception cref="DocumentServiceOperationCancelledException">
    ///     When the operation cancelled by <see cref="CancellationToken" />.
    /// </exception>
    /// <exception cref="UnknownErrorDocumentServiceException">
    ///     When any other error happens.
    /// </exception>
    Task<DocumentResult> AddAsync(DocumentInput inputInput,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns a <see cref="List{T}" /> of <see cref="DocumentInput" /> representing the <see cref="Document" />
    ///     entities in
    ///     the system.
    /// </summary>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>Returns a <see cref="Task{TResult}" /> representing the result of an asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">
    ///     When input is null.
    /// </exception>
    /// <exception cref="DocumentServiceOperationCancelledException">
    ///     When the operation is cancelled by a <see cref="CancellationToken" />
    /// </exception>
    /// <exception cref="UnknownErrorDocumentServiceException">
    ///     In case of any other errors
    /// </exception>
    Task<List<DocumentResult>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns with the indicated <see cref="Document" />'s values mapped to a <see cref="DocumentInput" />
    ///     object.
    /// </summary>
    /// <param name="id">The id value</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     Returns <see cref="Task{TResult}" /> representing result of an asynchronous operation. It includes the
    ///     <see cref="DocumentInput" /> result.
    /// </returns>
    /// <exception cref="InvalidInputToDocumentServiceException">
    ///     Invalid input provided to the service
    /// </exception>
    /// <exception cref="DocumentServiceOperationCancelledException">
    ///     When the operation is cancelled by a <see cref="CancellationToken" />.
    /// </exception>
    /// <exception cref="NoSuchItemDocumentServiceException">
    ///     When there is no such item in the system based on, probably, entity id.
    /// </exception>
    /// <exception cref="UnknownErrorDocumentServiceException">
    ///     In case of any other errors
    /// </exception>
    Task<DocumentResult> GetByIdAsync(long id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Modifies the designated <see cref="Document" /> entity in the system based on the provided
    ///     <see cref="DocumentInput" /> instance.
    /// </summary>
    /// <param name="documentId">The id of entity to be modified</param>
    /// <param name="modifiedInput">The provided changes</param>
    /// <returns>
    ///     Returns <see cref="Task{TResult}" /> representing result of an asynchronous operation.
    ///     It included the <see cref="DocumentInput" /> result.
    /// </returns>
    /// <exception cref="InvalidInputToDocumentServiceException">
    ///     Invalid input provided to the service
    /// </exception>
    /// <exception cref="DocumentServiceOperationCancelledException">
    ///     When the operation is cancelled by a <see cref="CancellationToken" />.
    /// </exception>
    /// <exception cref="NoSuchItemDocumentServiceException">
    ///     When there is no such item in the system based on, probably, entity id.
    /// </exception>
    /// <exception cref="UnknownErrorDocumentServiceException">
    ///     In case of any other errors
    /// </exception>
    Task<DocumentResult> UpdateAsync(long documentId, DocumentInput modifiedInput);

    /// <summary>
    ///     Deletes the designated <see cref="Document" /> entity.
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