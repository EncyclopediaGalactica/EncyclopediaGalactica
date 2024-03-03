namespace EncyclopediaGalactica.Services.Document.Service.Interfaces.Document;

using Contracts.Input;
using Contracts.Output;
using Entities;
using Exceptions;

public interface IAddDocumentScenario
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
}