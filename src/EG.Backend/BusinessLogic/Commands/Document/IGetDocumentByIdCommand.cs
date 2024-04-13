namespace EncyclopediaGalactica.BusinessLogic.Commands.Document;

using Contracts;
using Exceptions;

public interface IGetDocumentByIdCommand
{
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
    /// <exception cref="InvalidArgumentCommandException">
    ///     Invalid input provided to the service
    /// </exception>
    /// <exception cref="OperationCancelledCommandException">
    ///     When the operation is cancelled by a <see cref="CancellationToken" />.
    /// </exception>
    /// <exception cref="NoSuchItemCommandException">
    ///     When there is no such item in the system based on, probably, entity id.
    /// </exception>
    /// <exception cref="UnknownErrorCommandException">
    ///     In case of any other errors
    /// </exception>
    Task<DocumentResult> GetByIdAsync(long id, CancellationToken cancellationToken = default);
}