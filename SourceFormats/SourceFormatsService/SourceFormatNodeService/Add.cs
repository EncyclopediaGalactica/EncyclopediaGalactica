namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.SourceFormatNodeService;

using System.Runtime.CompilerServices;
using Dtos;
using Entities;
using Exceptions;
using FluentValidation;
using Mappers.Exceptions.SourceFormatNode;
using Microsoft.EntityFrameworkCore;
using Repository.Exceptions;
using Sdk.Models;
using SourceFormatsCacheService.Exceptions;
using ValidatorService;

public partial class SourceFormatNodeService
{
    public async Task<SourceFormatNodeAddResponseModel> AddAsync(
        SourceFormatNodeAddRequestModel addRequestModel,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await ValidateInputAsync(addRequestModel).ConfigureAwait(false);
            SourceFormatNode sourceFormatNode = MapSourceFormatNodeDtoToSourceFormatNode(addRequestModel);
            SourceFormatNode result = await PersistSourceFormatNodeAsync(sourceFormatNode, cancellationToken);
            await AppendToSourceFormatNodesCachedList(result, SourceFormatNodesListKey);
            SourceFormatNodeDto mappedResult = MapSourceFormatToSourceFormatNodeDto(result);
            return mappedResult;
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
            string msg = $"Input validation error at {nameof(SourceFormatNodeService)}.{nameof(AddAsync)}";
            throw new SourceFormatNodeServiceInputValidationException(msg, e);
        }
        // see the previous conditional catch why we have this bit complex condition here
        catch (Exception e) when (e is SourceFormatNodeMapperException
                                      or SourceFormatsCacheServiceException
                                  || e is SourceFormatNodeRepositoryException &&
                                  e.InnerException is not DbUpdateException)
        {
            string msg = $"Error happened while executing {nameof(SourceFormatNodeService)}.{nameof(AddAsync)}.";
            throw new SourceFormatNodeServiceException(msg, e);
        }
    }

    private SourceFormatNodeDto MapSourceFormatToSourceFormatNodeDto(SourceFormatNode node)
    {
        return _sourceFormatMappers
            .SourceFormatNodeMappers
            .MapSourceFormatNodeToSourceFormatNodeModelInFlatFashion(node);
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
        return _sourceFormatMappers.SourceFormatNodeMappers.MapSourceFormatNodeModelToSourceFormatNode(dto);
    }

    private async Task ValidateInputAsync(SourceFormatNodeAddRequestModel addRequestModel)
    {
        await _sourceFormatNodeAddModelValidator.ValidateAsync(addRequestModel, o =>
        {
            o.IncludeRuleSets(SourceFormatNodeAddModelValidator.Add);
            o.ThrowOnFailures();
        }).ConfigureAwait(false);
    }
}