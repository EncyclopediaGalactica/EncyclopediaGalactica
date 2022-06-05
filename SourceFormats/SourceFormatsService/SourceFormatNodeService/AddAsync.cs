namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.SourceFormatNodeService;

using Dtos;
using Entities;
using FluentValidation;
using Interfaces;
using Interfaces.SourceFormatNode;
using Mappers.Exceptions.SourceFormatNode;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SourceFormatsCacheService.Exceptions;
using SourceFormatsRepository.Exceptions;
using ValidatorService;

public partial class SourceFormatNodeService
{
    /// <inheritdoc />
    public async Task<SourceFormatNodeSingleResultResponseModel> AddAsync(
        SourceFormatNodeDto dto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(dto);
            await ValidateInputDataForAddingAsync(dto).ConfigureAwait(false);
            SourceFormatNode sourceFormatNode = MapSourceFormatNodeDtoToSourceFormatNode(dto);
            SourceFormatNode result = await PersistSourceFormatNodeAsync(sourceFormatNode, cancellationToken)
                .ConfigureAwait(false);
            //await AppendToSourceFormatNodesCachedList(result, SourceFormatNodesListKey);
            SourceFormatNodeDto mappedResult = MapSourceFormatNodeToSourceFormatNodeDto(result);
            SourceFormatNodeSingleResultResponseModel responseModel = PrepareSuccessResponseModelForAdd(mappedResult);

            _logger.LogInformation("{Method} is executed successfully", nameof(AddAsync));

            return responseModel;
        }
        // When Name UNIQUE constraint is violated a DbUpdate Exception is thrown
        // and it is wrapped in a SourceFormatNodeRepositoryException but it is still input validation context
        // so that exception is caught here and wrapped and re-thrown
        // so we can indicate that validation related error happened
        catch (Exception e) when (e is ArgumentNullException
                                      or ValidationException
                                  || (e is SourceFormatNodeRepositoryException
                                      && e.InnerException is DbUpdateException))

        {
            SourceFormatNodeSingleResultResponseModel validationErrorResponseModel =
                new()
                {
                    Status = SourceFormatsServiceResultStatuses.ValidationError,
                    IsOperationSuccessful = false
                };

            _logger.LogError("Validation error." +
                             "Method: {Method}. " +
                             "Message: {Message} " +
                             "Stacktrace: {StackTrace}",
                nameof(SourceFormatNodeService) + "." + nameof(AddAsync),
                e.Message,
                e.StackTrace);

            return validationErrorResponseModel;
        }
        // see the previous conditional catch why we have this bit complex condition here
        catch (Exception e) when (e is SourceFormatNodeMapperException
                                      or SourceFormatsCacheServiceException
                                  || (e is SourceFormatNodeRepositoryException &&
                                      e.InnerException is not DbUpdateException))
        {
            SourceFormatNodeSingleResultResponseModel internalErrorResponseModel =
                new()
                {
                    Status = SourceFormatsServiceResultStatuses.InternalError,
                    IsOperationSuccessful = false
                };

            _logger.LogError("Internal error." +
                             "Method: {Method}. " +
                             "Message: {Message} " +
                             "Stacktrace: {StackTrace}",
                nameof(SourceFormatNodeService) + "." + nameof(AddAsync),
                e.Message,
                e.StackTrace);

            return internalErrorResponseModel;
        }
        catch (Exception e)
        {
            SourceFormatNodeSingleResultResponseModel unexpectedResponseModel =
                new()
                {
                    Status = SourceFormatsServiceResultStatuses.InternalError,
                    IsOperationSuccessful = false
                };

            _logger.LogError("Internal error." +
                             "Method: {Method}. " +
                             "Message: {Message} " +
                             "Stacktrace: {StackTrace}",
                nameof(SourceFormatNodeService) + "." + nameof(AddAsync),
                e.Message,
                e.StackTrace);

            return unexpectedResponseModel;
        }
    }

    private SourceFormatNodeDto MapSourceFormatNodeToSourceFormatNodeDto(SourceFormatNode node)
    {
        return _sourceFormatMappers
            .SourceFormatNodeMappers
            .MapSourceFormatNodeToSourceFormatNodeDtoInFlatFashion(node);
    }

    private async Task AppendToSourceFormatNodesCachedList(SourceFormatNode node, string key)
    {
        await _sourceFormatNodeCacheService.AppendToCache(node, key, _cacheExpiresInMinutes);
    }

    private async Task<SourceFormatNode> PersistSourceFormatNodeAsync(
        SourceFormatNode newSourceFormatNode,
        CancellationToken cancellationToken)
    {
        SourceFormatNode result = await _sourceFormatNodeRepository.AddAsync(
                newSourceFormatNode,
                cancellationToken)
            .ConfigureAwait(false);
        return result;
    }

    private SourceFormatNode MapSourceFormatNodeDtoToSourceFormatNode(SourceFormatNodeDto dto)
    {
        return _sourceFormatMappers.SourceFormatNodeMappers.MapSourceFormatNodeDtoToSourceFormatNode(dto);
    }

    private async Task ValidateInputDataForAddingAsync(SourceFormatNodeDto dto)
    {
        await _sourceFormatNodeDtoValidator.ValidateAsync(dto, o =>
        {
            o.IncludeRuleSets(SourceFormatNodeDtoValidator.Add);
            o.ThrowOnFailures();
        }).ConfigureAwait(false);
    }

    private SourceFormatNodeSingleResultResponseModel PrepareSuccessResponseModelForAdd(SourceFormatNodeDto dto)
    {
        SourceFormatNodeSingleResultResponseModel responseModel = new()
        {
            Result = dto,
            Status = SourceFormatsServiceResultStatuses.Success,
            IsOperationSuccessful = true
        };
        return responseModel;
    }
}