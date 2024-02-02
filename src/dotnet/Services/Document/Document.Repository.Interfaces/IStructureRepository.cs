namespace EncyclopediaGalactica.Services.Document.Repository.Interfaces;

using Entities;

/// <summary>
///     IStructure repository
/// </summary>
public interface IStructureRepository
{
    /// <summary>
    ///     Creates a new <see cref="Structure" /> entity in the system and persists it in the database.
    /// </summary>
    /// <param name="structure">The new <see cref="Structure" /> object.</param>
    /// <param name="cancellationToken"><see cref="CancellationToken" />.</param>
    /// <returns>
    ///     Returns <see cref="Task{TResult}" /> representing result of asynchronous operation.
    /// </returns>
    Task<Structure> AddNewAsync(Structure structure, CancellationToken cancellationToken = default);
}