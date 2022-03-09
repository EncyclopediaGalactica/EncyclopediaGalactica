namespace EncyclopediaGalactica.SourceFormats.Worker.Repository.SourceFormatNode;

using Ctx;
using Entities;
using FluentValidation;
using Guards;
using Interfaces;

public partial class SourceFormatNodeRepository : ISourceFormatsNodeRepository
{
    private readonly IValidator<SourceFormatNode> _sourceFormatValidator;
    private readonly SourceFormatNodeDbContext _ctx;

    public SourceFormatNodeRepository(
        SourceFormatNodeDbContext ctx,
        IValidator<SourceFormatNode> sourceFormatValidator)
    {
        Guard.NotNull(sourceFormatValidator);
        Guard.NotNull(ctx);
        _ctx = ctx;
        _sourceFormatValidator = sourceFormatValidator;
    }

    public async Task<SourceFormatNode> AddChildNode(SourceFormatNode child, SourceFormatNode parent)
    {
        throw new NotImplementedException();
    }

    public SourceFormatNode Update(SourceFormatNode node)
    {
        throw new NotImplementedException();
    }

    public void Delete(SourceFormatNode node)
    {
        throw new NotImplementedException();
    }

    public ICollection<SourceFormatNode> GetAll()
    {
        throw new NotImplementedException();
    }

    public SourceFormatNode GetById(long id)
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