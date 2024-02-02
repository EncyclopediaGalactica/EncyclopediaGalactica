namespace EncyclopediaGalactica.Services.Document.Service.SourceFormatNodeService;

using Contracts.Input;
using Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;
using ValidatorService;

public partial class SourceFormatNodeService
{
    /// <inheritdoc />
    public async Task<SourceFormatNodeInput> AddAsync(
        SourceFormatNodeInput input,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(input);
        await ValidateInputDataForAddingAsync(input).ConfigureAwait(false);
        SourceFormatNode sourceFormatNode = MapSourceFormatNodeDtoToSourceFormatNode(input);
        SourceFormatNode result = await PersistSourceFormatNodeAsync(sourceFormatNode, cancellationToken)
            .ConfigureAwait(false);
        //await AppendToSourceFormatNodesCachedList(result, SourceFormatNodesListKey);
        SourceFormatNodeInput mappedResult = MapSourceFormatNodeToSourceFormatNodeDto(result);

        _logger.LogInformation("{Method} is executed successfully", nameof(AddAsync));

        return mappedResult;
    }

    private SourceFormatNodeInput MapSourceFormatNodeToSourceFormatNodeDto(SourceFormatNode node)
    {
        return _sourceFormatMappers
            .SourceFormatNodeMappers
            .MapSourceFormatNodeToSourceFormatNodeDtoInFlatFashion(node);
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

    private SourceFormatNode MapSourceFormatNodeDtoToSourceFormatNode(SourceFormatNodeInput input)
    {
        return _sourceFormatMappers.SourceFormatNodeMappers.MapSourceFormatNodeDtoToSourceFormatNode(input);
    }

    private async Task ValidateInputDataForAddingAsync(SourceFormatNodeInput input)
    {
        await _sourceFormatNodeDtoValidator.ValidateAsync(input, o =>
        {
            o.IncludeRuleSets(SourceFormatNodeDtoValidator.Add);
            o.ThrowOnFailures();
        }).ConfigureAwait(false);
    }
}