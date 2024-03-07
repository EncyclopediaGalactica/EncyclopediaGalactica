namespace EncyclopediaGalactica.Services.Document.Repository.Structure;

using Ctx;
using Entities;
using Exceptions;
using FluentValidation;
using ValidatorService;

public partial class StructureNodeRepository
{
    /// <inheritdoc />
    public async Task<StructureNode> AddNewAsync(
        StructureNode structureNode,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await AddNewBusinessLogicAsync(structureNode, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception e)
        {
            string m = $"{nameof(AddNewAsync)} operation failed. " +
                       $"For further details see the inner exception details.";
            throw new StructureNodeRepositoryException(
                m,
                e);
        }
    }

    private async Task<StructureNode> AddNewBusinessLogicAsync(
        StructureNode structureNode,
        CancellationToken cancellationToken = default)
    {
        await ValidateInputAsync(structureNode, cancellationToken).ConfigureAwait(false);
        return await RecordNewItemAsync(structureNode).ConfigureAwait(false);
    }

    private async Task<StructureNode> RecordNewItemAsync(StructureNode structureNode)
    {
        await using DocumentDbContext ctx = new(_dbContextOptions);
        ctx.StructureNodes.Add(structureNode);
        await ctx.SaveChangesAsync().ConfigureAwait(false);
        return structureNode;
    }

    private async Task ValidateInputAsync(
        StructureNode structureNode,
        CancellationToken cancellationToken = default)
    {
        await _structureNodeValidator.ValidateAsync(structureNode, o =>
            {
                o.IncludeRuleSets(Operations.Add);
                o.ThrowOnFailures();
            }, cancellationToken
        ).ConfigureAwait(false);
    }
}