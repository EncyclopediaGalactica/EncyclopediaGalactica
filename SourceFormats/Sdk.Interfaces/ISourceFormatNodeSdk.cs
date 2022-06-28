namespace EncyclopediaGalactica.SourceFormats.Sdk.Interfaces;

using Dtos;
using Exceptions;
using Models.SourceFormatNode;

public interface ISourceFormatNodeSdk
{
    /// <summary>
    ///     Calls the Encyclopedia Galactica SourceFormats endpoint and returns a
    ///     <see cref="SourceFormatNodeGetAllResponseModel" /> which includes information
    ///     about the operation result and the actual result of operation.
    /// </summary>
    /// <param name="requestModel">
    ///     <see cref="SourceFormatNodeGetAllRequestModel" />
    /// </param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     Returns <see cref="Task{TResult}" /> representing asynchronous operation which includes the result.
    /// </returns>
    /// <exception cref="SdkException">In case of any error</exception>
    Task<SourceFormatNodeGetAllResponseModel> GetAllAsync(
        SourceFormatNodeGetAllRequestModel requestModel,
        CancellationToken cancellationToken = default);

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

    /// <summary>
    ///     Sends an <see cref="SourceFormatNodeUpdateRequestModel" /> to the Encyclopedia Galactica Source Format endpoint
    ///     via PUT Http Method.
    ///     As a result the system is going to update the defined (by the id) entity with the provided properties.
    ///     The request includes a <see cref="SourceFormatNodeDto" /> describing changes.
    /// </summary>
    /// <param name="updateRequestModel">The model marking which entity should be updated and the desired state after change</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     Returns a <see cref="Task{TResult}" /> representing result of asynchronous operation, it also
    ///     includes the result which is a <see cref="SourceFormatNodeUpdateResponseModel" /> providing details
    ///     about the operation and its result.
    /// </returns>
    /// <exception cref="SdkException">In case of any error.</exception>
    Task<SourceFormatNodeUpdateResponseModel> UpdateAsync(
        SourceFormatNodeUpdateRequestModel updateRequestModel,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Sends a <see cref="SourceFormatNodeDeleteRequestModel" /> to the Encyclopedia Galactica Source Format endpoint
    ///     via DELETE Http method.
    ///     As a result the system deletes the marked entity from the system. If the entity has any related entity, mainly
    ///     children, they also will be deleted.
    ///     Using the model object our users also can manipulate the accepted headers of the request.
    /// </summary>
    /// <param name="deleteRequestModel">The request model containing all information</param>
    /// <param name="cancellationToken">
    ///     <see cref="CancellationToken" />
    /// </param>
    /// <returns>
    ///     Returns a <see cref="Task{TResult}" /> representing result of asynchronous operation which includes
    ///     a result object (<see cref="SourceFormatNodeDeleteResponseModel" />) having all details of the operation
    ///     and its result.
    /// </returns>
    Task<SourceFormatNodeDeleteResponseModel> DeleteAsync(
        SourceFormatNodeDeleteRequestModel deleteRequestModel,
        CancellationToken cancellationToken = default);
}