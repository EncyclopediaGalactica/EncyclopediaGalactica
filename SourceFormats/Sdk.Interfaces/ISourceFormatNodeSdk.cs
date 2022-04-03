namespace EncyclopediaGalactica.SourceFormats.Sdk.Interfaces;

using Dtos;
using Exceptions;
using Models;

public interface ISourceFormatNodeSdk
{
    /// <summary>
    ///     Calls the Encyclopedia Galactica SourceFormats endpoint and returns a
    ///     <see cref="List{T}" /> of <see cref="SourceFormatNodeDto" />s.
    /// </summary>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     Returns <see cref="Task{T}" /> representing asynchronous operation which includes the result.
    /// </returns>
    /// <exception cref="SdkException">In case of any error</exception>
    Task<List<SourceFormatNodeDto>?> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Post a <see cref="SourceFormatNodeAddRequestModel" /> to the Encyclopedia Galactica SourceFormat Node endpoint.
    ///     As a result the endpoint is going to create a new SourceFormatNode entity. The newly created entity
    ///     properties are sent via the model.
    ///     Once the new entity is created its data will be returned in a <see cref="SourceFormatNodeAddResponseModel" />.
    /// </summary>
    /// <param name="addRequestModel">The <see cref="SourceFormatNodeAddRequestModel" /></param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     Returns <see cref="Task{Result}" /> representing asynchronous operation, it also includes the
    ///     result. The result is a <see cref="SourceFormatNodeAddResponseModel" /> which provides details about the
    ///     operation and if there is operation result, then it is also available.
    /// </returns>
    /// <exception cref="SdkException">In case of any error.</exception>
    Task<SourceFormatNodeAddResponseModel> AddAsync(
        SourceFormatNodeAddRequestModel addRequestModel,
        CancellationToken cancellationToken = default);
}