namespace EncyclopediaGalactica.Services.Document.Service.Interfaces.Document;

using Contracts.Input;
using Contracts.Output;
using Entities;
using Exceptions;

public interface IUpdateDocumentScenario
{
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
}