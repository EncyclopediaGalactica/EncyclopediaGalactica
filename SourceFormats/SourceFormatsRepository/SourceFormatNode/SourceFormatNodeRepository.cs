namespace EncyclopediaGalactica.SourceFormats.SourceFormatsRepository.SourceFormatNode;

using Ctx;
using Entities;
using FluentValidation;
using Interfaces;
using Utils.GuardsService;

public partial class SourceFormatNodeRepository : ISourceFormatNodeRepository
{
    private readonly SourceFormatsDbContext _ctx;
    private readonly IGuardsService _guards;
    private readonly IValidator<SourceFormatNode> _sourceFormatNodeValidator;

    public SourceFormatNodeRepository(
        SourceFormatsDbContext ctx,
        IValidator<SourceFormatNode> sourceFormatNodeValidator,
        IGuardsService guardsService)
    {
        _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
        _sourceFormatNodeValidator = sourceFormatNodeValidator ??
                                     throw new ArgumentNullException(nameof(sourceFormatNodeValidator));
        _guards = guardsService ?? throw new ArgumentNullException(nameof(guardsService));
    }

    private string prepErrorMessage(string methodName)
    {
        string msg = $"Error occured while executing {nameof(SourceFormatNodeRepository)}.{methodName}. " +
                     "For further information see inner exception!";
        return msg;
    }
}