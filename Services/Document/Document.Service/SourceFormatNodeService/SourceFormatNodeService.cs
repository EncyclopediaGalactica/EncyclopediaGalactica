namespace EncyclopediaGalactica.Services.Document.Service.SourceFormatNodeService;

using Contracts.Input;
using Entities;
using FluentValidation;
using Interfaces.SourceFormatNode;
using Mappers.Interfaces;
using Microsoft.Extensions.Logging;
using Repository.Interfaces;
using Utils.GuardsService.Interfaces;

public partial class SourceFormatNodeService : ISourceFormatNodeService
{
    private const string SourceFormatNodesListKey = "SourceFormatNodesList";
    private readonly int _cacheExpiresInMinutes = 60;
    private readonly IGuardsService _guards;
    private readonly ILogger _logger;
    private readonly ISourceFormatMappers _sourceFormatMappers;
    private readonly IValidator<SourceFormatNodeInputContract> _sourceFormatNodeDtoValidator;
    private readonly ISourceFormatNodeRepository _sourceFormatNodeRepository;

    public SourceFormatNodeService(
        IValidator<SourceFormatNodeInputContract> sourceFormatNodeDtoValidator,
        IGuardsService guardsService,
        ISourceFormatMappers sourceFormatMappers,
        ISourceFormatNodeRepository sourceFormatNodeRepository,
        ILogger<SourceFormatNodeService> logger)
    {
        ArgumentNullException.ThrowIfNull(sourceFormatNodeDtoValidator);
        ArgumentNullException.ThrowIfNull(guardsService);
        ArgumentNullException.ThrowIfNull(sourceFormatMappers);
        ArgumentNullException.ThrowIfNull(sourceFormatNodeRepository);
        ArgumentNullException.ThrowIfNull(logger);

        _sourceFormatNodeDtoValidator = sourceFormatNodeDtoValidator;
        _guards = guardsService;
        _sourceFormatMappers = sourceFormatMappers;
        _sourceFormatNodeRepository = sourceFormatNodeRepository;
        _logger = logger;
    }

    public async Task<SourceFormatNodeInputContract> GetSourceFormatNodeByIdWithChildrenAsync(long id,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<SourceFormatNodeInputContract> GetSourceFormatNodeByIdWithNodeTreeAsync(long id,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<SourceFormatNode>> GetSourceFormatNodesAsync(
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}