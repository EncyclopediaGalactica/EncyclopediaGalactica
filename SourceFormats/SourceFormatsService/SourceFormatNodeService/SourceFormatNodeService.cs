namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.SourceFormatNodeService;

using Dtos;
using Entities;
using FluentValidation;
using Interfaces;
using Mappers.Interfaces;
using Repository.Interfaces;
using Sdk.Models;
using SourceFormatsCacheService.Interfaces;
using Utils.GuardsService;

public partial class SourceFormatNodeService : ISourceFormatNodeService
{
    private const string SourceFormatNodesListKey = "SourceFormatNodesList";
    private readonly IGuardsService _guards;
    private readonly ISourceFormatMappers _sourceFormatMappers;
    private readonly IValidator<SourceFormatNodeAddRequestModel> _sourceFormatNodeAddModelValidator;
    private readonly ISourceFormatNodeCacheService _sourceFormatNodeCacheService;
    private readonly ISourceFormatNodeRepository _sourceFormatNodeRepository;
    private int _cacheExpiresInMinutes = 60;

    public SourceFormatNodeService(
        IValidator<SourceFormatNodeAddRequestModel> sourceFormatNodeAddModelValidator,
        IGuardsService guardsService,
        ISourceFormatMappers sourceFormatMappers,
        ISourceFormatNodeRepository sourceFormatNodeRepository,
        ISourceFormatNodeCacheService sourceFormatNodeCacheService)
    {
        _sourceFormatNodeAddModelValidator = sourceFormatNodeAddModelValidator ??
                                             throw new ArgumentNullException(nameof(sourceFormatNodeAddModelValidator));
        _guards = guardsService ?? throw new ArgumentNullException(nameof(guardsService));
        _sourceFormatMappers = sourceFormatMappers ?? throw new ArgumentNullException(nameof(sourceFormatMappers));
        _sourceFormatNodeRepository = sourceFormatNodeRepository ??
                                      throw new ArgumentNullException(nameof(sourceFormatNodeRepository));
        _sourceFormatNodeCacheService = sourceFormatNodeCacheService ??
                                        throw new ArgumentNullException(nameof(sourceFormatNodeCacheService));
    }

    public async Task<SourceFormatNodeDto> AddSourceFormatNodeChildToParent(SourceFormatNodeDto childDto,
        SourceFormatNodeDto parentDto,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<SourceFormatNode> GetSourceFormatNodeByIdAsync(long id,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<SourceFormatNodeDto> GetSourceFormatNodeByIdWithChildrenAsync(long id,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<SourceFormatNodeDto> GetSourceFormatNodeByIdWithNodeTreeAsync(long id,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<SourceFormatNode>> GetSourceFormatNodesAsync(
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<SourceFormatNodeDto> UpdateSourceFormatNodeAsync(SourceFormatNodeDto dto,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteSourceFormatNodeAsync(SourceFormatNodeDto dto,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}