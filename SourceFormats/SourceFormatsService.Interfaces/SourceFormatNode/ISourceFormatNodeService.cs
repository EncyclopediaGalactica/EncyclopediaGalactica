namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Interfaces.SourceFormatNode;

using Dtos;
using Entities;
using Exceptions;
using Sdk.Models.SourceFormatNode;

public interface ISourceFormatNodeService
{
    /// <summary>
    ///     Creates a new <see cref="SourceFormatNode" /> in the system based on the data stored
    ///     in the provided input <see cref="SourceFormatNodeDto" />.
    /// </summary>
    /// <param name="dto"><see cref="SourceFormatNodeDto" /> contains the details of the new entity.</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     Returns a <see cref="Task{TResult}" /> representing asynchronous operation. Which includes
    ///     a <see cref="SourceFormatNodeAddResponseModel" /> object as result.
    /// </returns>
    /// <exception cref="SourceFormatNodeServiceException">
    ///     Whenever internal error happens which
    ///     doesn't include input validation.
    /// </exception>
    /// <exception cref="SourceFormatNodeServiceInputValidationException">
    ///     Whenever input validation related
    ///     error happens.
    /// </exception>
    Task<SourceFormatNodeSingleResultResponseModel> AddAsync(
        SourceFormatNodeDto dto,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Adds the given <see cref="SourceFormatNode" /> to another <see cref="SourceFormatNode" />
    ///     as child.
    /// </summary>
    /// <param name="childDto">The child</param>
    /// <param name="parentDto">The parent</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     Returns <see cref="Task{TResult}" /> representing result of asynchronous operation, which includes
    ///     a <see cref="SourceFormatNodeSingleResultResponseModel" /> which provides details about
    ///     is the operation was successful and returns with the child <see cref="SourceFormatNode" />.
    /// </returns>
    /// <exception cref="Exception">All exceptions are caught</exception>
    Task<SourceFormatNodeSingleResultResponseModel> AddChildToParentAsync(
        SourceFormatNodeDto childDto,
        SourceFormatNodeDto parentDto,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Returns the details of the designated <see cref="SourceFormatNode" />
    ///     object
    /// </summary>
    /// <param name="id">Unique identifier of the object</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     Returns <see cref="Task{TResult}" /> representing result of asynchronous operation.
    ///     It also includes a <see cref="SourceFormatNodeSingleResultResponseModel" /> where
    ///     the properties provide additional information about the result of the operation and
    ///     the result of the operation.
    /// </returns>
    Task<SourceFormatNodeSingleResultResponseModel> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default);

    Task<SourceFormatNodeDto> GetSourceFormatNodeByIdWithChildrenAsync(
        long id,
        CancellationToken cancellationToken = default);

    Task<SourceFormatNodeDto> GetSourceFormatNodeByIdWithNodeTreeAsync(
        long id,
        CancellationToken cancellationToken = default);

    Task<ICollection<SourceFormatNode>> GetSourceFormatNodesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Updates the defined <see cref="SourceFormatNode" /> entity in the system.
    ///     The entity properties will be overwritten by the provided object's properties.
    ///     The entity going to be overwritten is defined by the input object Id property.
    /// </summary>
    /// <param name="dto">The object defines which entity properties will be overwritten by this object properties</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     Returns <see cref="Task{TResult}" /> representing asynchronous operation where the Result is
    ///     <see cref="SourceFormatNodeUpdateResponseModel" /> providing the result of the operation and the
    ///     updated entity with its new values.
    /// </returns>
    /// <exception cref="SourceFormatNodeServiceException">In case of any error</exception>
    Task<SourceFormatNodeSingleResultResponseModel> UpdateSourceFormatNodeAsync(
        SourceFormatNodeDto? dto,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Deletes the marked <see cref="SourceFormatNode" />.
    ///     If the <see cref="SourceFormatNode" /> has children nodes then those will be deleted too.
    ///     The system does not allow that a <see cref="SourceFormatNode" /> is attached to multiple parents.
    /// </summary>
    /// <param name="dto">The entity should be deleted</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     Returns a <see cref="Task{TResult}" /> representing asynchronous result which
    ///     includes a <see cref="SourceFormatNodeSingleResultResponseModel" /> indicating the result of the operation.
    /// </returns>
    /// <exception cref="Exception">All the exceptions are caught</exception>
    Task<SourceFormatNodeSingleResultResponseModel> DeleteAsync(
        SourceFormatNodeDto dto,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Requests all of <see cref="SourceFormatNode" />s from the system.
    ///     The returned nodes do not have their navigation properties populated. It is a flat list.
    /// </summary>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     Returns <see cref="Task{TResult}" /> representing result of asynchronous operation which includes
    ///     the result. The result type is <see cref="SourceFormatNodeGetAllResponseModel" /> which includes
    ///     information of execution and result.
    /// </returns>
    /// <exception cref="SourceFormatNodeServiceException">If any error happen.</exception>
    Task<SourceFormatNodeListResultResponseModel> GetAllAsync(CancellationToken cancellationToken = default);
}