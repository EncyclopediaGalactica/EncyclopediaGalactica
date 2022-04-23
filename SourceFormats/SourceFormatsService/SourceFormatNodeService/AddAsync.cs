namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.SourceFormatNodeService;

using System.Net;
using System.Runtime.CompilerServices;
using Dtos;
using Entities;
using FluentValidation;
using Mappers.Exceptions.SourceFormatNode;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Exceptions;
using Sdk.Models.SourceFormatNode;
using SourceFormatsCacheService.Exceptions;
using ValidatorService;

public partial class SourceFormatNodeService
{
    public async Task<SourceFormatNodeAddResponseModel> AddAsync(
        SourceFormatNodeDto dto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(dto);
            await ValidateInputDataAsync(dto).ConfigureAwait(false);
            SourceFormatNode sourceFormatNode = MapSourceFormatNodeDtoToSourceFormatNode(dto);
            SourceFormatNode result = await PersistSourceFormatNodeAsync(sourceFormatNode, cancellationToken);
            await AppendToSourceFormatNodesCachedList(result, SourceFormatNodesListKey);
            SourceFormatNodeDto mappedResult = MapSourceFormatToSourceFormatNodeDto(result);
            SourceFormatNodeAddResponseModel responseModel = PrepareSuccessResponseModel(mappedResult);
            return responseModel;
        }
        // When Name UNIQUE constraint is violated a DbUpdate Exception is thrown
        // and it is wrapped in a SourceFormatNodeRepositoryException but it is still input validation context
        // so that exception is caught here and wrapped and re-thrown
        // so we can indicate that validation related error happened
        catch (Exception e) when (e is ArgumentNullException
                                      or ValidationException
                                  || e is SourceFormatNodeRepositoryException
                                  && e.InnerException is DbUpdateException)

        {
            SourceFormatNodeAddResponseModel validationErrorResponseModel =
                new SourceFormatNodeAddResponseModel.Builder()
                    .SetMessage("Validation error.")
                    .SetHttpStatusCode(HttpStatusCode.BadRequest)
                    .SetOperationFailed()
                    .Build();

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
                                  || e is SourceFormatNodeRepositoryException &&
                                  e.InnerException is not DbUpdateException)
        {
            SourceFormatNodeAddResponseModel internalErrorResponseModel = new SourceFormatNodeAddResponseModel.Builder()
                .SetHttpStatusCode(HttpStatusCode.InternalServerError)
                .SetOperationFailed()
                .SetMessage("Internal Server Error")
                .Build();

            _logger.LogError("Internal error." +
                             "Method: {Method}. " +
                             "Message: {Message} " +
                             "Stacktrace: {StackTrace}",
                nameof(SourceFormatNodeService) + "." + nameof(AddAsync),
                e.Message,
                e.StackTrace);

            return internalErrorResponseModel;
        }
    }

    private SourceFormatNodeDto MapSourceFormatToSourceFormatNodeDto(SourceFormatNode node)
    {
        return _sourceFormatMappers
            .SourceFormatNodeMappers
            .MapSourceFormatNodeToSourceFormatNodeDtoInFlatFashion(node);
    }

    private async Task AppendToSourceFormatNodesCachedList(SourceFormatNode node, string key)
    {
        await _sourceFormatNodeCacheService.AppendToCache(node, key, _cacheExpiresInMinutes);
    }

    private ConfiguredTaskAwaitable<SourceFormatNode> PersistSourceFormatNodeAsync(
        SourceFormatNode sourceFormatNode,
        CancellationToken cancellationToken)
    {
        return _sourceFormatNodeRepository.AddAsync(
                sourceFormatNode,
                cancellationToken)
            .ConfigureAwait(false);
    }

    private SourceFormatNode MapSourceFormatNodeDtoToSourceFormatNode(SourceFormatNodeDto dto)
    {
        return _sourceFormatMappers.SourceFormatNodeMappers.MapSourceFormatNodeDtoToSourceFormatNode(dto);
    }

    private async Task ValidateInputDataAsync(SourceFormatNodeDto dto)
    {
        await _sourceFormatNodeDtoValidator.ValidateAsync(dto, o =>
        {
            o.IncludeRuleSets(SourceFormatNodeDtoValidator.Add);
            o.ThrowOnFailures();
        }).ConfigureAwait(false);
    }
}