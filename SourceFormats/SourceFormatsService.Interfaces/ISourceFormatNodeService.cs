namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Interfaces;

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
    Task<SourceFormatNodeAddResponseModel> AddAsync(
        SourceFormatNodeDto dto,
        CancellationToken cancellationToken = default);

    Task<SourceFormatNodeDto> AddSourceFormatNodeChildToParent(
        SourceFormatNodeDto childDto,
        SourceFormatNodeDto parentDto,
        CancellationToken cancellationToken);

    Task<SourceFormatNode> GetSourceFormatNodeByIdAsync(long id, CancellationToken cancellationToken = default);

    Task<SourceFormatNodeDto> GetSourceFormatNodeByIdWithChildrenAsync(
        long id,
        CancellationToken cancellationToken = default);

    Task<SourceFormatNodeDto> GetSourceFormatNodeByIdWithNodeTreeAsync(
        long id,
        CancellationToken cancellationToken = default);

    Task<ICollection<SourceFormatNode>> GetSourceFormatNodesAsync(CancellationToken cancellationToken = default);

    Task<SourceFormatNodeDto> UpdateSourceFormatNodeAsync(
        SourceFormatNodeDto dto,
        CancellationToken cancellationToken = default);

    Task DeleteSourceFormatNodeAsync(SourceFormatNodeDto dto, CancellationToken cancellationToken = default);

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
    Task<SourceFormatNodeGetAllResponseModel> GetAllAsync(CancellationToken cancellationToken = default);
}