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

    private string prepErrorMessage(string methodName)
    {
        string msg = $"Error occured while executing {nameof(SourceFormatNodeRepository)}.{methodName}. " +
                     $"For further information see inner exception!";
        return msg;
    }
}