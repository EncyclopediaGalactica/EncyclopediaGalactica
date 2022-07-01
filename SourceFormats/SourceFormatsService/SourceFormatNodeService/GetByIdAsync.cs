namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.SourceFormatNodeService;

using Dtos;
using Entities;
using Interfaces;
using Interfaces.SourceFormatNode;
using Utils.GuardsService.Exceptions;

public partial class SourceFormatNodeService
{
    /// <inheritdoc />
    public async Task<SourceFormatNodeSingleResultResponseModel> GetByIdAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _guards.IsNotEqual(id, 0);
            SourceFormatNode result = await _sourceFormatNodeRepository.GetByIdWithRootNodeAsync(id, cancellationToken)
                .ConfigureAwait(false);
            SourceFormatNodeDto resultDto = _sourceFormatMappers.SourceFormatNodeMappers
                .MapSourceFormatNodeToSourceFormatNodeDto(result);

            SourceFormatNodeSingleResultResponseModel resultResponseModel =
                new SourceFormatNodeSingleResultResponseModel
                {
                    Result = resultDto,
                    IsOperationSuccessful = true,
                    Status = SourceFormatsServiceResultStatuses.Success
                };
            return resultResponseModel;
        }
        catch (Exception inputValidationException) when
            (inputValidationException is GuardsServiceValueShouldNotBeEqualToException or
                ArgumentNullException)
        {
            // logging comes here
            SourceFormatNodeSingleResultResponseModel inputValidationErrorResponseModel =
                new SourceFormatNodeSingleResultResponseModel
                {
                    IsOperationSuccessful = false,
                    Result = null,
                    Status = SourceFormatsServiceResultStatuses.ValidationError
                };
            return inputValidationErrorResponseModel;
        }
    }
}