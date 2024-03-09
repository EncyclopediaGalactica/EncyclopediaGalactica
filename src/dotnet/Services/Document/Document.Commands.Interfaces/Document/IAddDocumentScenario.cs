namespace EncyclopediaGalactica.Services.Document.Scenario.Interfaces.Document;

using Contracts.Input;
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
    ///     Returns a <see cref="Task{TResult}" /> object representing the result of an asynchronous operation which in this
    ///     case is the id of the newly created entity.
    /// </returns>
    /// <exception cref="InvalidInputToDocumentServiceException">
    ///     When invalid input is provided to the service
    /// </exception>
    /// <exception cref="DocumentServiceOperationCancelledException">
    ///     When the operation cancelled by <see cref="CancellationToken" />.
    /// </exception>
    /// <exception cref="UnknownErrorScenarioException">
    ///     When any other error happens.
    /// </exception>
    Task<long> AddAsync(DocumentInput inputInput, CancellationToken cancellationToken = default);
}