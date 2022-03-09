namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.SourceFormatNode;

using Entities;
using Exceptions;
using FluentValidation;
using ValidatorService;

public partial class SourceFormatNodeRepository
{
    public async Task<SourceFormatNode> AddAsync(
        SourceFormatNode node,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await ValidateInputNodeForAdding(node, cancellationToken).ConfigureAwait(false);
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

    private async Task ValidateInputNodeForAdding(SourceFormatNode node, CancellationToken cancellationToken)
    {
        await _sourceFormatValidator.ValidateAsync(node, o =>
            {
                o.IncludeRuleSets(SourceFormatNodeValidator.Add);
                o.ThrowOnFailures();
            }, cancellationToken)
            .ConfigureAwait(false);
    }
}