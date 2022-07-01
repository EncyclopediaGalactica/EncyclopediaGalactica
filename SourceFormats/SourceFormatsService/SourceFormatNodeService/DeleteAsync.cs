namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.SourceFormatNodeService;

using Dtos;
using Interfaces;
using Interfaces.SourceFormatNode;
using Utils.GuardsService.Exceptions;

public partial class SourceFormatNodeService
{
    /// <inheritdoc />
    public async Task<SourceFormatNodeSingleResultResponseModel> DeleteAsync(
        SourceFormatNodeDto dto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _guards.NotNull(dto);
            _guards.IsNotEqual(dto.Id, 0);

            await _sourceFormatNodeRepository.DeleteAsync(dto.Id, cancellationToken).ConfigureAwait(false);

            SourceFormatNodeSingleResultResponseModel responseModel = PrepareSuccessResponseModelForDelete();
            return responseModel;
        }
        catch (Exception validationException) when (validationException is GuardsServiceValueShouldNoBeNullException
                                                        or GuardsServiceValueShouldNotBeEqualToException
                                                        or ArgumentNullException)
        {
            // logging comes here
            SourceFormatNodeSingleResultResponseModel validationExceptionResponseModel =
                new SourceFormatNodeSingleResultResponseModel
                {
                    Result = null,
                    IsOperationSuccessful = false,
                    Status = SourceFormatsServiceResultStatuses.ValidationError
                };
            return validationExceptionResponseModel;
        }
        catch (Exception noSuchEntityException) when (noSuchEntityException is InvalidOperationException)
        {
            // logging comes here
            SourceFormatNodeSingleResultResponseModel noSuchEntityResponseModel =
                new SourceFormatNodeSingleResultResponseModel
                {
                    Result = null,
                    IsOperationSuccessful = false,
                    Status = SourceFormatsServiceResultStatuses.NoSuchEntity
                };
            return noSuchEntityResponseModel;
        }
    }

    private SourceFormatNodeSingleResultResponseModel PrepareSuccessResponseModelForDelete()
    {
        SourceFormatNodeSingleResultResponseModel responseModel = new SourceFormatNodeSingleResultResponseModel
        {
            Result = null,
            Status = SourceFormatsServiceResultStatuses.Success,
            IsOperationSuccessful = true
        };

        return responseModel;
    }
}