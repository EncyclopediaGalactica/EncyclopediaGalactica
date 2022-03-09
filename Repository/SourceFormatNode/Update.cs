namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.SourceFormatNode;

using Entities;
using Exceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ValidatorService;

public partial class SourceFormatNodeRepository
{
    public async Task<SourceFormatNode> UpdateAsync(
        SourceFormatNode node,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await _ctx.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);
            await ValidateInputForUpdatingAsync(node, cancellationToken).ConfigureAwait(false);
            SourceFormatNode? toBeUpdated = await _ctx.SourceFormatNodes.FindAsync(
                node.Id).ConfigureAwait(false);

            if (toBeUpdated is null)
                throw new SourceFormatNodeRepositoryException($"No entity with id {node.Id}");

            await MapNewValuesToEntity(node, toBeUpdated);
            _ctx.Entry(toBeUpdated).State = EntityState.Modified;
            await _ctx.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            await _ctx.Database.CommitTransactionAsync(cancellationToken).ConfigureAwait(false);
            return toBeUpdated;
        }
        catch (Exception e)
        {
            await _ctx.Database.RollbackTransactionAsync(cancellationToken).ConfigureAwait(false);
            string msg = $"Exception happened while executing {nameof(SourceFormatNodeRepository)}." +
                         $"{nameof(UpdateAsync)}. For further information see inner exception!";
            throw new SourceFormatNodeRepositoryException(msg, e);
        }
    }

    private async Task MapNewValuesToEntity(SourceFormatNode node, SourceFormatNode toBeUpdated)
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