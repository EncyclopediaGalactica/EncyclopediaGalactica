namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.SourceFormatNode;

using Ctx;
using Entities;
using FluentValidation;
using Guards;
using Interfaces;

public partial class SourceFormatNodeRepository : ISourceFormatsNodeRepository
{
    private readonly IValidator<SourceFormatNode> _sourceFormatNodeValidator;
    private readonly SourceFormatNodeDbContext _ctx;

    public SourceFormatNodeRepository(
        SourceFormatNodeDbContext ctx,
        IValidator<SourceFormatNode> sourceFormatNodeValidator)
    {
        Guard.NotNull(sourceFormatNodeValidator);
        Guard.NotNull(ctx);
        _ctx = ctx;
        _sourceFormatNodeValidator = sourceFormatNodeValidator;
    }

    public async Task<SourceFormatNode> AddChildNode(SourceFormatNode child, SourceFormatNode parent)
    {
        throw new NotImplementedException();
    }

    public void Delete(SourceFormatNode node)
    {
        throw new NotImplementedException();
    }

    public async Task<SourceFormatNode> GetNodeWithChildren(SourceFormatNode node)
    {
        throw new NotImplementedException();
    }

    public async Task<SourceFormatNode> GetNodeWithTree(SourceFormatNode node)
    {
        throw new NotImplementedException();
    }
}