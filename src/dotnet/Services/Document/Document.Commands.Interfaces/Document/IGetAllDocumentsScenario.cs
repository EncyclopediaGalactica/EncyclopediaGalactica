namespace EncyclopediaGalactica.Services.Document.Scenario.Interfaces.Document;

using Contracts.Input;
using Contracts.Output;
using Exceptions;

public interface IGetAllDocumentsScenario
{
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
    /// <exception cref="UnknownErrorScenarioException">
    ///     In case of any other errors
    /// </exception>
    Task<List<DocumentResult>> GetAllAsync(CancellationToken cancellationToken = default);
}