namespace EncyclopediaGalactica.BusinessLogic.Commands.StructureNode;

using Contracts;
using Exceptions;

/// <summary>
///     Add a New Structure Node to the system.
/// </summary>
public interface IAddNewStructureNodeCommand
{
    /// <summary>
    ///     Creates a new <see cref="StructureNode" /> to the system based on the provided information in the
    ///     <see cref="StructureNodeInput" />.
    /// </summary>
    /// <param name="structureNodeInput">The <see cref="StructureNodeInput" /> providing details</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     <see cref="Task{TResult}" /> representing result of asynchronous operation.
    /// </returns>
    /// <exception cref="InvalidArgumentCommandException">
    ///     When the command receives invalid input.
    /// </exception>
    /// <exception cref="OperationCancelledCommandException">
    ///     When the operation is cancelled by receiving a <see cref="CancellationToken" />.
    /// </exception>
    /// <exception cref="UnknownErrorCommandException">
    ///     When the execution stops with an unknown error.
    /// </exception>
    Task AddNewAsync(StructureNodeInput structureNodeInput, CancellationToken cancellationToken = default);
}