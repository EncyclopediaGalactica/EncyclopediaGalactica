#region

#endregion

namespace EncyclopediaGalactica.DocumentDomain.Operations.Commands;

using Common.Commands.Exceptions;
using Entity;

public interface IDeleteDocumentCommand
{
    /// <summary>
    ///     Deletes the designated <see cref="Document" /> entity.
    /// </summary>
    /// <param name="documentId">The unique identifier of the <see cref="Document" /> entity to be deleted.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <exception cref="InvalidArgumentCommandException">
    ///     Invalid input provided to the service
    /// </exception>
    /// <exception cref="OperationCancelledCommandException">
    ///     When the operation is cancelled by a <see cref="CancellationToken" />.
    /// </exception>
    /// <exception cref="NoSuchItemCommandException">
    ///     When there is no such entity in the system based on its unique identifier.
    /// </exception>
    /// <exception cref="UnknownErrorCommandException">
    ///     In case of any other errors.
    /// </exception>
    Task DeleteAsync(long documentId, CancellationToken cancellationToken = default);
}