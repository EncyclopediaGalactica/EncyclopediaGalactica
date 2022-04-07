namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.SourceFormatNodeService;

using System.Runtime.CompilerServices;
using Dtos;
using Entities;
using FluentValidation;
using Mappers.Exceptions.SourceFormatNode;
using Microsoft.EntityFrameworkCore;
using Repository.Exceptions;
using Sdk.Models.SourceFormatNode;
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
            ValidateInputModel(addRequestModel);
            await ValidateInputDataAsync(addRequestModel.Payload).ConfigureAwait(false);
            SourceFormatNode sourceFormatNode = MapSourceFormatNodeDtoToSourceFormatNode(addRequestModel.Payload);
            SourceFormatNode result = await PersistSourceFormatNodeAsync(sourceFormatNode, cancellationToken);
            await AppendToSourceFormatNodesCachedList(result, SourceFormatNodesListKey);
            SourceFormatNodeDto mappedResult = MapSourceFormatToSourceFormatNodeDto(result);
            SourceFormatNodeAddResponseModel responseModel = PrepareResponseModel(mappedResult);
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
                new SourceFormatNodeAddResponseModel();
            validationErrorResponseModel.Message = "Validation error.";
            validationErrorResponseModel.HttpStatusCode = 400;
            return validationErrorResponseModel;
        }
        // see the previous conditional catch why we have this bit complex condition here
        catch (Exception e) when (e is SourceFormatNodeMapperException
                                      or SourceFormatsCacheServiceException
                                  || e is SourceFormatNodeRepositoryException &&
                                  e.InnerException is not DbUpdateException)
        {
            SourceFormatNodeAddResponseModel internalErrorResponseModel =
                new SourceFormatNodeAddResponseModel();
            internalErrorResponseModel.Message = "Internal error.";
            internalErrorResponseModel.HttpStatusCode = 500;
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

    private void ValidateInputModel(SourceFormatNodeAddRequestModel addRequestModel)
    {
        if (addRequestModel is null)
            throw new ArgumentNullException(nameof(addRequestModel));
        if (addRequestModel.Payload is null)
            throw new ArgumentNullException(nameof(addRequestModel.Payload));
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