namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.SourceFormatNodeService;

using Dtos;
using Entities;
using Mappers.Exceptions.SourceFormatNode;
using Microsoft.Extensions.Logging;
using Repository.Exceptions;
using Sdk.Models.SourceFormatNode;

public partial class SourceFormatNodeService
{
    /// <inheritdoc />
    public async Task<SourceFormatNodeGetAllResponseModel> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            // TODO: figure out how caching can play a role here, especially caching strategies
            List<SourceFormatNode> sourceFormatNodes = await _sourceFormatNodeRepository
                .GetAllAsync(cancellationToken)
                .ConfigureAwait(false);
            List<SourceFormatNodeDto> mapped = _sourceFormatMappers.SourceFormatNodeMappers
                .MapSourceFormatNodesToSourceFormatNodeDtosInFlatFashion(sourceFormatNodes);
            SourceFormatNodeGetAllResponseModel responseModel = new SourceFormatNodeGetAllResponseModel
            {
                IsOperationSuccessful = true,
                Result = mapped,
                HttpStatusCode = 200
            };
            _logger.LogInformation($"{nameof(SourceFormatNodeService)}.{nameof(GetAllAsync)} executed.");
            return responseModel;
        }
        catch (Exception e) when (
            e is SourceFormatNodeRepositoryException or SourceFormatNodeMapperException)
        {
            SourceFormatNodeGetAllResponseModel responseModel = new SourceFormatNodeGetAllResponseModel
            {
                IsOperationSuccessful = false,
                Result = null,
                HttpStatusCode = 500,
                Message = "Error happened while executing business logic. For further details see logs."
            };
            _logger.LogError("Business logic error happened. " +
                             "Method name: {MethodName}. " +
                             "Exception message: {ExceptionMessage}. " +
                             "Stack trace: {StackTrace}",
                nameof(SourceFormatNodeService) + "." + nameof(GetAllAsync),
                e.Message,
                e.StackTrace);
            return responseModel;
        }
        catch (Exception e)
        {
            SourceFormatNodeGetAllResponseModel responseModel = new SourceFormatNodeGetAllResponseModel
            {
                IsOperationSuccessful = false,
                Result = null,
                HttpStatusCode = 500,
                Message = "Unknown error happened. For further details see logs."
            };

            _logger.LogError("Unknown error happened. " +
                             "Method name: {MethodName}. " +
                             "Exception message: {ExceptionMessage}. " +
                             "Stack trace: {StackTrace}",
                nameof(SourceFormatNodeService) + "." + nameof(GetAllAsync),
                e.Message,
                e.StackTrace);


            return responseModel;
        }
    }
}