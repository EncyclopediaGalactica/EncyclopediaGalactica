namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.SourceFormatNodeService;

using Dtos;
using Entities;
using Interfaces;
using Interfaces.SourceFormatNode;
using Mappers.Exceptions.SourceFormatNode;
using Microsoft.Extensions.Logging;
using SourceFormatsRepository.Exceptions;

public partial class SourceFormatNodeService
{
    /// <inheritdoc />
    public async Task<SourceFormatNodeListResultResponseModel> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            // TODO: figure out how caching can play a role here, especially caching strategies
            List<SourceFormatNode> sourceFormatNodes = await _sourceFormatNodeRepository
                .GetAllAsync(cancellationToken)
                .ConfigureAwait(false);
            List<SourceFormatNodeDto> mapped = _sourceFormatMappers.SourceFormatNodeMappers
                .MapSourceFormatNodesToSourceFormatNodeDtosInFlatFashion(sourceFormatNodes);
            SourceFormatNodeListResultResponseModel responseModel = PrepareSuccessResponseModelForGetAll(mapped);

            _logger.LogInformation($"{nameof(SourceFormatNodeService)}.{nameof(GetAllAsync)} executed.");
            return responseModel;
        }
        catch (Exception e) when (
            e is SourceFormatNodeRepositoryException or SourceFormatNodeMapperException)
        {
            SourceFormatNodeListResultResponseModel responseModel = new()
            {
                IsOperationSuccessful = false,
                Result = null,
                Status = SourceFormatsServiceResultStatuses.InternalError
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
            SourceFormatNodeListResultResponseModel responseModel = new()
            {
                IsOperationSuccessful = false,
                Result = null,
                Status = SourceFormatsServiceResultStatuses.InternalError
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

    private SourceFormatNodeListResultResponseModel PrepareSuccessResponseModelForGetAll(
        List<SourceFormatNodeDto> mapped)
    {
        SourceFormatNodeListResultResponseModel responseModel = new()
        {
            IsOperationSuccessful = true,
            Result = mapped,
            Status = SourceFormatsServiceResultStatuses.Success
        };
        return responseModel;
    }
}