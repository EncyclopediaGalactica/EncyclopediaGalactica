namespace EncyclopediaGalactica.Services.Document.Service.SourceFormatNodeService;

using Contracts.Input;
using Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;
using ValidatorService;

public partial class SourceFormatNodeService
{
    /// <inheritdoc />
    public async Task<SourceFormatNodeInputContract> AddAsync(
        SourceFormatNodeInputContract inputContract,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(inputContract);
        await ValidateInputDataForAddingAsync(inputContract).ConfigureAwait(false);
        SourceFormatNode sourceFormatNode = MapSourceFormatNodeDtoToSourceFormatNode(inputContract);
        SourceFormatNode result = await PersistSourceFormatNodeAsync(sourceFormatNode, cancellationToken)
            .ConfigureAwait(false);
        //await AppendToSourceFormatNodesCachedList(result, SourceFormatNodesListKey);
        SourceFormatNodeInputContract mappedResult = MapSourceFormatNodeToSourceFormatNodeDto(result);

        _logger.LogInformation("{Method} is executed successfully", nameof(AddAsync));

        return mappedResult;
    }

    private SourceFormatNodeInputContract MapSourceFormatNodeToSourceFormatNodeDto(SourceFormatNode node)
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

    private SourceFormatNode MapSourceFormatNodeDtoToSourceFormatNode(SourceFormatNodeInputContract inputContract)
    {
        return _sourceFormatMappers.SourceFormatNodeMappers.MapSourceFormatNodeDtoToSourceFormatNode(inputContract);
    }

    private async Task ValidateInputDataForAddingAsync(SourceFormatNodeInputContract inputContract)
    {
        await _sourceFormatNodeDtoValidator.ValidateAsync(inputContract, o =>
        {
            o.IncludeRuleSets(SourceFormatNodeDtoValidator.Add);
            o.ThrowOnFailures();
        }).ConfigureAwait(false);
    }
}