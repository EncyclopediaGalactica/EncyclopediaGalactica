namespace EncyclopediaGalactica.DocumentDomain.Operations.Commands;

using Common.Commands.Exceptions;
using EncyclopediaGalactica.Common.Contracts;

/// <summary>
///     Add Structure Node Tree Scenario.
///     <remarks>
///         This scenario covers the case when a new <see cref="Document" /> is created with a <see cref="StructureNode" />
///         tree, or the <see cref="StructureNode" /> is modified and the whole tree is added to the database in
///         one go. Whatever <see cref="StructureNode" /> tree structure is passed to the method the first node
///         will be considered as root node and attached to the designated <see cref="Document" />.
///     </remarks>
/// </summary>
public interface IAddStructureNodeTreeCommand
{
    /// <summary>
    ///     Adds <see cref="StructureNode" /> tree to the designated <see cref="Document" />.
    /// </summary>
    /// <param name="documentId">The document id.</param>
    /// <param name="structureNodeInput">The <see cref="StructureNodeInput" /> including the tree.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns></returns>
    /// <exception cref="InvalidArgumentCommandException">
    ///     When the command receives invalid input.
    /// </exception>
    /// <exception cref="OperationCancelledCommandException">
    ///     When the command operation is cancelled by receiving a <see cref="CancellationToken" />.
    /// </exception>
    /// <exception cref="UnknownErrorCommandException">
    ///     When unknown error happens during command execution.
    /// </exception>
    Task AddTreeAsync(
        long documentId,
        DocumentStructureNodeInput structureNodeInput,
        CancellationToken cancellationToken = default);
}