namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.SourceFormatNodeService;

using Dtos;
using Entities;
using Exceptions;
using FluentValidation;
using Interfaces;
using Interfaces.SourceFormatNode;
using Mappers.Exceptions.SourceFormatNode;
using Microsoft.Extensions.Logging;
using SourceFormatsRepository.Exceptions;
using ValidatorService;

public partial class SourceFormatNodeService
{
    /// <inheritdoc />
    public async Task<SourceFormatNodeSingleResultResponseModel> UpdateSourceFormatNodeAsync(
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
            SourceFormatNodeSingleResultResponseModel responseModel = PrepareSuccessResponseModelForUpdate(updatedDto);
            return responseModel;
        }
        catch (Exception e) when (e is ArgumentNullException or ValidationException)
        {
            _logger.LogWarning("{Operation} failed due to validation error", nameof(UpdateSourceFormatNodeAsync));

            SourceFormatNodeSingleResultResponseModel responseModel = new()
            {
                Status = SourceFormatsServiceResultStatuses.ValidationError,
                Result = null,
                IsOperationSuccessful = false
            };
            return responseModel;
        }
        catch (Exception e) when (e is SourceFormatNodeRepositoryException &&
                                  e.InnerException is InvalidOperationException)
        {
            _logger.LogWarning("{Operation} failed due to there is no item in the sequence",
                nameof(UpdateSourceFormatNodeAsync));
            SourceFormatNodeSingleResultResponseModel responseModel = new()
            {
                Status = SourceFormatsServiceResultStatuses.NoSuchEntity,
                IsOperationSuccessful = false,
                Result = null
            };
            return responseModel;
        }
        catch (Exception e) when (e is SourceFormatNodeMapperException or SourceFormatNodeServiceException)
        {
            _logger.LogWarning("{Operation} failed due to internal error", nameof(UpdateSourceFormatNodeAsync));
            SourceFormatNodeSingleResultResponseModel responseModel = new()
            {
                Status = SourceFormatsServiceResultStatuses.InternalError,
                IsOperationSuccessful = false,
                Result = null
            };
            return responseModel;
        }
        catch (Exception e)
        {
            _logger.LogWarning("{Operation} failed due to something unexpected", nameof(UpdateSourceFormatNodeAsync));
            SourceFormatNodeSingleResultResponseModel responseModel = new()
            {
                Status = SourceFormatsServiceResultStatuses.InternalError,
                IsOperationSuccessful = false,
                Result = null
            };
            return responseModel;
        }
    }

    private SourceFormatNodeSingleResultResponseModel PrepareSuccessResponseModelForUpdate(
        SourceFormatNodeDto resultDto)
    {
        SourceFormatNodeSingleResultResponseModel responseModel = new()
        {
            Result = resultDto,
            IsOperationSuccessful = true,
            Status = SourceFormatsServiceResultStatuses.Success
        };
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