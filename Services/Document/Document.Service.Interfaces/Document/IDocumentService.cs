namespace EncyclopediaGalactica.Services.Document.SourceFormatsService.Interfaces.Document;

using Dtos;
using Entities;
using Exceptions;

/// <summary>
///     IDocument IAM.Service interface.
///     <remarks>
///         The service provides IAM.Api methods to access <see cref="Document" /> objects stored in the system.
///     </remarks>
/// </summary>
public interface IDocumentService
{
    /// <summary>
    ///     Adds a <see cref="Document" /> object to the system with the values represented by the provided
    ///     <see cref="DocumentDto" />.
    /// </summary>
    /// <param name="dtoInput">The input object</param>
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
    Task<DocumentDto> AddAsync(DocumentDto dtoInput, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns a <see cref="List{T}" /> of <see cref="DocumentDto" /> representing the <see cref="Document" /> entities in
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
    Task<List<DocumentDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns with the indicated <see cref="Document" />'s values mapped to a <see cref="DocumentDto" /> object.
    /// </summary>
    /// <param name="id">The id value</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     Returns <see cref="Task{TResult}" /> representing result of an asynchronous operation. It includes the
    ///     <see cref="DocumentDto" /> result.
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
    Task<DocumentDto> GetByIdAsync(long id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Modifies the designated <see cref="Document" /> entity in the system based on the provided
    ///     <see cref="DocumentDto" /> instance.
    /// </summary>
    /// <param name="documentId">The id of entity to be modified</param>
    /// <param name="modifiedDto">The provided changes</param>
    /// <returns>
    ///     Returns <see cref="Task{TResult}" /> representing result of an asynchronous operation.
    ///     It included the <see cref="DocumentDto" /> result.
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
    Task<DocumentDto> UpdateAsync(long documentId, DocumentDto modifiedDto);
}