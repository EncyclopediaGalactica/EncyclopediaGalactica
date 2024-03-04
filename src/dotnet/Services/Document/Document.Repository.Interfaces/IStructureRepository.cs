namespace EncyclopediaGalactica.Services.Document.Repository.Interfaces;

using Entities;

/// <summary>
///     IStructure repository
/// </summary>
public interface IStructureRepository
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
}