namespace EncyclopediaGalactica.SourceFormats.Repository.SourceFormatNode;

using Ctx;
using Entities;
using FluentValidation;
using Guards;
using Interfaces;

public partial class SourceFormatNodeRepository : ISourceFormatsNodeRepository
{
    private readonly SourceFormatNodeDbContext _ctx;
    private readonly IGuardService _guard;
    private readonly IValidator<SourceFormatNode> _sourceFormatNodeValidator;

    public SourceFormatNodeRepository(
        SourceFormatNodeDbContext ctx,
        IValidator<SourceFormatNode> sourceFormatNodeValidator,
        IGuardService guardService)
    {
        _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
        _sourceFormatNodeValidator = sourceFormatNodeValidator ??
                                     throw new ArgumentNullException(nameof(sourceFormatNodeValidator));
        _guard = guardService ?? throw new ArgumentNullException(nameof(guardService));
    }

    private string prepErrorMessage(string methodName)
    {
        string msg = $"Error occured while executing {nameof(SourceFormatNodeRepository)}.{methodName}. " +
                     "For further information see inner exception!";
        return msg;
    }
}