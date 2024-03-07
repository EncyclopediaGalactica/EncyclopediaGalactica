namespace EncyclopediaGalactica.Services.Document.Repository.Interfaces;

using Entities;

/// <summary>
///     IStructure repository
/// </summary>
public interface IStructureNodeRepository
{
    /// <summary>
    ///     Creates a new <see cref="StructureNode" /> entity in the system and persists it in the database.
    /// </summary>
    /// <param name="structureNode">The new <see cref="StructureNode" /> object.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns>
    ///     Returns <see cref="Task{TResult}" /> representing result of asynchronous operation.
    /// </returns>
    Task<StructureNode> AddNewAsync(StructureNode structureNode, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns the list <see cref="StructureNode" /> entity which is a root node and has the document id designated by the
    ///     input parameter.
    /// </summary>
    /// <param name="documentId">The document id.</param>
    /// <returns>The <see cref="StructureNode" /> entity.</returns>
    /// <exception cref="ArgumentNullException">
    ///     If source or predicate is null or empty
    /// </exception>
    /// <exception cref="InvalidOperationException">
    ///     No element satisfies the condition.
    /// </exception>
    /// <exception cref="OperationCanceledException">
    ///     When the operation is cancelled by <see cref="CancellationToken" />
    /// </exception>
    Task<List<StructureNode>> GetRootNodesByDocumentIdAsync(long documentId);
}