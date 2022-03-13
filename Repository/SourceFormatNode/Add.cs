namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.SourceFormatNode;

using Entities;
using Exceptions;
using FluentValidation;
using Guards;
using ValidatorService;

public partial class SourceFormatNodeRepository
{
    public async Task<SourceFormatNode> AddAsync(
        SourceFormatNode node,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await ValidateInputNodeForAddingAsync(node, cancellationToken).ConfigureAwait(false);
            _ctx.SourceFormatNodes.Add(node);
            await _ctx.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return node;
        }
        catch (Exception e)
        {
            string msg = $"Exception was thrown while {nameof(SourceFormatNodeRepository)}.{nameof(AddAsync)} " +
                         $"was executed. For further information see inner exception.";
            throw new SourceFormatNodeRepositoryException(msg, e);
        }
    }

    private async Task ValidateInputNodeForAddingAsync(SourceFormatNode node, CancellationToken cancellationToken)
    {
        Guard.NotNull(node);
        await _sourceFormatNodeValidator.ValidateAsync(node, o =>
            {
                o.IncludeRuleSets(SourceFormatNodeValidator.Add);
                o.ThrowOnFailures();
            }, cancellationToken)
            .ConfigureAwait(false);
    }
}