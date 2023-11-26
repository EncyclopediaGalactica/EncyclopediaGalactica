namespace EncyclopediaGalactica.Services.Document.Service.SourceFormatNodeService;

using Contracts.Input;
using Entities;
using FluentValidation;
using ValidatorService;

public partial class SourceFormatNodeService
{
    /// <inheritdoc />
    public async Task<SourceFormatNodeInput> UpdateSourceFormatNodeAsync(
        SourceFormatNodeInput? dto,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto);
        _guards.IsNotEqual(dto.Id, 0);
        await ValidateInputDataForUpdateAsync(dto).ConfigureAwait(false);
        SourceFormatNode updateTemplate = MapSourceFormatNodeDtoToSourceFormatNode(dto);
        SourceFormatNode updated = await _sourceFormatNodeRepository.UpdateAsync(updateTemplate, cancellationToken)
            .ConfigureAwait(false);
        // TODO: caching!
        return MapSourceFormatNodeToSourceFormatNodeDto(updated);
    }

    private async Task ValidateInputDataForUpdateAsync(SourceFormatNodeInput inputInput)
    {
        await _sourceFormatNodeDtoValidator.ValidateAsync(inputInput, options =>
        {
            options.IncludeRuleSets(SourceFormatNodeDtoValidator.Update);
            options.ThrowOnFailures();
        }).ConfigureAwait(false);
    }
}