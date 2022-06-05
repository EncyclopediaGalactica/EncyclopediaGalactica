namespace EncyclopediaGalactica.SourceFormats.SourceFormatsRepository.SourceFormatNode;

using Entities;
using FluentValidation;
using ValidatorService;

public partial class SourceFormatNodeRepository
{
    /// <inheritdoc />
    public async Task<SourceFormatNode> AddAsync(
        SourceFormatNode node,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(node);
        await ValidateInputNodeForAddingAsync(node, cancellationToken).ConfigureAwait(false);
        _ctx.SourceFormatNodes.Add(node);
        await _ctx.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return node;
    }

    private async Task ValidateInputNodeForAddingAsync(SourceFormatNode node, CancellationToken cancellationToken)
    {
        await _sourceFormatNodeValidator.ValidateAsync(node, o =>
            {
                o.IncludeRuleSets(SourceFormatNodeValidator.Add);
                o.ThrowOnFailures();
            }, cancellationToken)
            .ConfigureAwait(false);
    }
}