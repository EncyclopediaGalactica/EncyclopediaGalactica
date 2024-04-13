namespace EncyclopediaGalactica.BusinessLogic.Commands.Document;

using Contracts;
using Exceptions;

public interface IGetAllDocumentsCommand
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
    /// <exception cref="OperationCancelledCommandException">
    ///     When the operation is cancelled by a <see cref="CancellationToken" />
    /// </exception>
    /// <exception cref="UnknownErrorCommandException">
    ///     In case of any other errors
    /// </exception>
    Task<List<DocumentResult>> GetAllAsync(CancellationToken cancellationToken = default);
}