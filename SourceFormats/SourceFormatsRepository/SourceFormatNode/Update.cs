namespace EncyclopediaGalactica.SourceFormats.SourceFormatsRepository.SourceFormatNode;

using Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Repository.Exceptions;
using ValidatorService;

public partial class SourceFormatNodeRepository
{
    /// <inheritdoc />
    public async Task<SourceFormatNode> UpdateAsync(
        SourceFormatNode node,
        CancellationToken cancellationToken = default)
    {
        try
        {
#pragma warning disable CA1062
            await ValidateInputForUpdatingAsync(node, cancellationToken).ConfigureAwait(false);
            SourceFormatNode? toBeUpdated = await _ctx.SourceFormatNodes
                .FirstAsync(p => p.Id == node.Id, cancellationToken)
                .ConfigureAwait(false);
#pragma warning restore CA1062

            MapNewValuesToEntity(node, toBeUpdated);
            _ctx.Entry(toBeUpdated).State = EntityState.Modified;
            await _ctx.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return toBeUpdated;
        }
        catch (Exception e)
        {
            string msg = prepErrorMessage(nameof(UpdateAsync));
            throw new SourceFormatNodeRepositoryException(msg, e);
        }
    }

    private static void MapNewValuesToEntity(SourceFormatNode node, SourceFormatNode toBeUpdated)
    {
        toBeUpdated.Name = node.Name;
        toBeUpdated.IsRootNode = node.IsRootNode;
        toBeUpdated.ParentNodeId = node.ParentNodeId;
        toBeUpdated.RootNodeId = node.RootNodeId;
    }

    private async Task ValidateInputForUpdatingAsync(SourceFormatNode node, CancellationToken cancellationToken)
    {
        await _sourceFormatNodeValidator.ValidateAsync(node, o =>
        {
            o.IncludeRuleSets(SourceFormatNodeValidator.Update);
            o.ThrowOnFailures();
        }, cancellationToken).ConfigureAwait(false);
    }
}