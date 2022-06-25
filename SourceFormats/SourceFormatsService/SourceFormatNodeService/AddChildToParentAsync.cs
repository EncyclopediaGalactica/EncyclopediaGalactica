namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.SourceFormatNodeService;

using Dtos;
using Entities;
using Interfaces;
using Interfaces.SourceFormatNode;
using Utils.GuardsService.Exceptions;

public partial class SourceFormatNodeService
{
    /// <inheritdoc />
    public async Task<SourceFormatNodeSingleResultResponseModel> AddChildToParentAsync(
        SourceFormatNodeDto childDto,
        SourceFormatNodeDto parentDto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _guards.NotNull(childDto);
            _guards.NotNull(parentDto);
            _guards.IsNotEqual(childDto.Id, 0);
            _guards.IsNotEqual(parentDto.Id, 0);
            _guards.IsNotEqual(parentDto.Id, childDto.Id);

            SourceFormatNode rootNode = await _sourceFormatNodeRepository.GetByIdWithRootNodeAsync(
                    parentDto.Id,
                    cancellationToken)
                .ConfigureAwait(false);
            SourceFormatNode resultNode = await _sourceFormatNodeRepository.AddChildNodeAsync(
                    childDto.Id,
                    parentDto.Id,
                    rootNode.Id,
                    cancellationToken)
                .ConfigureAwait(false);
            SourceFormatNodeDto resultDto = _sourceFormatMappers.SourceFormatNodeMappers
                .MapSourceFormatNodeToSourceFormatNodeDtoInFlatFashion(resultNode);

            SourceFormatNodeSingleResultResponseModel responseModel = new SourceFormatNodeSingleResultResponseModel
            {
                IsOperationSuccessful = true,
                Result = resultDto,
                Status = SourceFormatsServiceResultStatuses.Success
            };
            return responseModel;
        }
        catch (Exception validationException) when (validationException is
                                                        GuardsServiceValueShouldNotBeEqualToException or
                                                        GuardsServiceValueShouldNoBeNullException or
                                                        ArgumentNullException)
        {
            // logging comes here
            SourceFormatNodeSingleResultResponseModel validationExceptionResultModel =
                new SourceFormatNodeSingleResultResponseModel
                {
                    IsOperationSuccessful = false,
                    Result = null,
                    Status = SourceFormatsServiceResultStatuses.ValidationError
                };
            return validationExceptionResultModel;
        }
        catch (Exception invalidOperationException) when (invalidOperationException is
                                                              InvalidOperationException)
        {
            // logging comes here
            SourceFormatNodeSingleResultResponseModel invalidOperationExceptionResponseModel =
                new SourceFormatNodeSingleResultResponseModel
                {
                    IsOperationSuccessful = false,
                    Result = null,
                    Status = SourceFormatsServiceResultStatuses.NoSuchEntity
                };
            return invalidOperationExceptionResponseModel;
        }
        catch (Exception operationCancelledException) when (operationCancelledException is OperationCanceledException)
        {
            // logging comes here
            SourceFormatNodeSingleResultResponseModel operationCancelledExceptionResponseModel =
                new SourceFormatNodeSingleResultResponseModel
                {
                    IsOperationSuccessful = false,
                    Result = null,
                    Status = SourceFormatsServiceResultStatuses.InternalError
                };
            return operationCancelledExceptionResponseModel;
        }
        finally
        {
            // logging comes here
        }
    }
}