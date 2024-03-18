namespace EncyclopediaGalactica.BusinessLogic.Commands.StructureNode;

using Contracts;

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
    ///     Adds the provided tree to the system.
    /// </summary>
    /// <param name="documentId"></param>
    /// <param name="structureNodeInput">The <see cref="StructureNode" /> tree.</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    Task AddTreeAsync(
        long documentId,
        StructureNodeInput structureNodeInput,
        CancellationToken cancellationToken = default);
}