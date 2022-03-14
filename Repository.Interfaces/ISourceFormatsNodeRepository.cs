namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.Interfaces;

using Entities;
using Exceptions;

/// <summary>
///     SourceFormatRepository interface for managing stored data in the database.
/// </summary>
public interface ISourceFormatsNodeRepository
{
    /// <summary>
    ///     Adds a new entity to the database.
    /// </summary>
    /// <param name="node">Object with details of the new entity.</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken">Cancellation token.</see>
    /// </param>
    /// <exception cref="SourceFormatNodeRepositoryException">
    ///     Whenever error occurs. Inner exceptions provide additional
    ///     information about the error.
    /// </exception>
    /// <returns>
    ///     Returns <see cref="Task" /> which includes the new created <see cref="SourceFormatNode" />
    ///     entity.
    /// </returns>
    Task<SourceFormatNode> AddAsync(SourceFormatNode node, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Adds a <see cref="SourceFormatNode" /> to another <see cref="SourceFormatNode" /> as child node.
    /// </summary>
    /// <param name="childId">Id of the entity which will be added as child.</param>
    /// <param name="parentId">Id of the parent entity</param>
    /// <param name="rootNodeId">Id of the root node</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <exception cref="SourceFormatNodeRepositoryException">
    ///     In case of an error and its inner exceptions provides additional details about the error.
    /// </exception>
    /// <returns>
    ///     Returns <see cref="Task" /> representing asynchronous operation which includes
    ///     the child object.
    /// </returns>
    Task<SourceFormatNode> AddChildNodeAsync(
        long childId,
        long parentId,
        long rootNodeId,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Updates the defined <see cref="SourceFormatNode" /> entity by the provided object property values.
    /// </summary>
    /// <param name="node">Object containing the new values.</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <exception cref="SourceFormatNodeRepositoryException">
    ///     In case of any error and its enclosed exceptions provide additional details
    ///     about the error.
    /// </exception>
    /// <returns>
    ///     Returns <see cref="Task" /> representing asynchronous operation which includes the updated
    ///     <see cref="SourceFormatNode" />
    ///     entity.
    /// </returns>
    Task<SourceFormatNode> UpdateAsync(SourceFormatNode node, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Deletes the defined <see cref="SourceFormatNode" /> entity.
    /// </summary>
    /// <param name="id">Identifier of the entity will be deleted</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <exception cref="SourceFormatNodeRepositoryException">
    ///     In case any error and its enclosed exceptions provide
    ///     additional information about the error.
    /// </exception>
    /// <returns>
    ///     Returns <see cref="Task" /> representing asynchronous operation.
    /// </returns>
    Task DeleteAsync(long id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns all <see cref="SourceFormatNode" /> entities as <see cref="List{T}" />.
    /// </summary>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <exception cref="SourceFormatNodeRepositoryException">
    ///     In case of any error and its inner exceptions provide additional details
    ///     about the error.
    /// </exception>
    /// <returns>
    ///     Returns <see cref="Task" /> representing asynchronous operation which includes the result.
    /// </returns>
    Task<List<SourceFormatNode>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns the identified <see cref="SourceFormatNode" /> entity with its children entities.
    /// </summary>
    /// <param name="id">Identifier of the desired entity.</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <exception cref="SourceFormatNodeRepositoryException">
    ///     In case of any error and its inner exceptions provide
    ///     further details about the error.
    /// </exception>
    /// <returns>
    ///     Returns <see cref="Task" /> representing asynchronous operation which encloses the result.
    /// </returns>
    Task<SourceFormatNode> GetByIdWithChildrenAsync(long id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns the identified <see cref="SourceFormatNode" /> entity without its related entities.
    /// </summary>
    /// <param name="id">Identifier of the desired entity.</param>
    /// <exception cref="SourceFormatNodeRepositoryException">
    ///     In case of any error and its inner exceptions provide
    ///     further details on the error occured.
    /// </exception>
    /// <returns>
    ///     Returns <see cref="Task" /> representing asynchronous operation which encloses the result.
    /// </returns>
    Task<SourceFormatNode> GetByIdAsync(long id);

    /// <summary>
    ///     Returns a flatten (partial) tree which starting point is the identified <see cref="SourceFormatNode" />
    ///     entity. The flatten tree means a list of items belong to the root <see cref="SourceFormatNode" />.
    ///     Navigation property of <see cref="SourceFormatNode" /> considering which entities are child of an entity
    ///     are populated.
    /// </summary>
    /// <param name="id">Identifier of the desired entity</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <exception cref="SourceFormatNodeRepositoryException">
    ///     In case of any error and its inner exceptions provide
    ///     additional details on the error occured.
    /// </exception>
    /// <returns>
    ///     Returns <see cref="Task" /> representing asynchronous operation which includes the result.
    /// </returns>
    Task<List<SourceFormatNode>> GetByIdWithFlatTreeAsync(long id, CancellationToken cancellationToken = default);
}