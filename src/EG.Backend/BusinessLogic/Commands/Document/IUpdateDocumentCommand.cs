namespace EncyclopediaGalactica.BusinessLogic.Commands.Document;

using Contracts;
using Exceptions;

public interface IUpdateDocumentCommand
{
    /// <summary>
    ///     Modifies the designated <see cref="Document" /> entity in the system based on the provided
    ///     <see cref="DocumentInput" /> instance.
    /// </summary>
    /// <param name="modifiedInput">The provided changes</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
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
    Task UpdateAsync(DocumentInput modifiedInput, CancellationToken cancellationToken = default);
}