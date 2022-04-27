namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.SourceFormatNodeService;

using System.Net;
using Dtos;
using Entities;
using Exceptions;
using FluentValidation;
using Mappers.Exceptions.SourceFormatNode;
using Microsoft.Extensions.Logging;
using Sdk.Models.SourceFormatNode;
using ValidatorService;

public partial class SourceFormatNodeService
{
    /// <inheritdoc />
    public async Task<SourceFormatNodeUpdateResponseModel> UpdateSourceFormatNodeAsync(
        SourceFormatNodeDto? dto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(dto);
            await ValidateInputDataForUpdateAsync(dto).ConfigureAwait(false);
            SourceFormatNode updateTemplate = MapSourceFormatNodeDtoToSourceFormatNode(dto);
            SourceFormatNode updated = await _sourceFormatNodeRepository.UpdateAsync(updateTemplate, cancellationToken)
                .ConfigureAwait(false);
            // TODO: caching!
            SourceFormatNodeDto updatedDto = MapSourceFormatNodeToSourceFormatNodeDto(updated);
            SourceFormatNodeUpdateResponseModel responseModel = PrepareSuccessResponseModelForUpdate(updatedDto);
            return responseModel;
        }
        catch (Exception e) when (e is ArgumentNullException or ValidationException)
        {
            _logger.LogWarning("{Operation} failed due to validation error", nameof(UpdateSourceFormatNodeAsync));

            SourceFormatNodeUpdateResponseModel responseModel = new SourceFormatNodeUpdateResponseModel.Builder()
                .SetMessage("Validation error")
                .SetOperationFailed()
                .SetHttpStatusCode(HttpStatusCode.BadRequest)
                .Build();
            return responseModel;
        }
        catch (Exception e) when (e is SourceFormatNodeMapperException or SourceFormatNodeServiceException)
        {
            _logger.LogWarning("{Operation} failed due to internal error", nameof(UpdateSourceFormatNodeAsync));
            SourceFormatNodeUpdateResponseModel responseModel = new SourceFormatNodeUpdateResponseModel.Builder()
                .SetMessage("Internal error")
                .SetOperationFailed()
                .SetHttpStatusCode(HttpStatusCode.InternalServerError)
                .Build();
            return responseModel;
        }
        catch (Exception e)
        {
            _logger.LogWarning("{Operation} failed due to something unexpected", nameof(UpdateSourceFormatNodeAsync));
            SourceFormatNodeUpdateResponseModel responseModel = new SourceFormatNodeUpdateResponseModel.Builder()
                .SetMessage("Unexpected error")
                .SetOperationFailed()
                .SetHttpStatusCode(HttpStatusCode.InternalServerError)
                .Build();
            return responseModel;
        }
    }

    private SourceFormatNodeUpdateResponseModel PrepareSuccessResponseModelForUpdate(SourceFormatNodeDto resultDto)
    {
        SourceFormatNodeUpdateResponseModel responseModel = new SourceFormatNodeUpdateResponseModel.Builder()
            .SetResult(resultDto)
            .SetOperationSuccessful()
            .SetHttpStatusCode(HttpStatusCode.OK)
            .Build();
        return responseModel;
    }

    private async Task ValidateInputDataForUpdateAsync(SourceFormatNodeDto inputDto)
    {
        await _sourceFormatNodeDtoValidator.ValidateAsync(inputDto, options =>
        {
            options.IncludeRuleSets(SourceFormatNodeDtoValidator.Update);
            options.ThrowOnFailures();
        }).ConfigureAwait(false);
    }
}