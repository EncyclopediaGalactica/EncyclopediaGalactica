namespace EncyclopediaGalactica.DocumentDomain.Operations.Commands;

using Common.Commands.Exceptions;
using EncyclopediaGalactica.Common.Contracts;

public interface IAddDocumentCommand
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
    /// <exception cref="InvalidArgumentCommandException">
    ///     When invalid input is provided to the service
    /// </exception>
    /// <exception cref="OperationCancelledCommandException">
    ///     When the operation cancelled by <see cref="CancellationToken" />.
    /// </exception>
    /// <exception cref="UnknownErrorCommandException">
    ///     When any other error happens.
    /// </exception>
    Task<long> AddAsync(DocumentInput inputInput, CancellationToken cancellationToken = default);
}