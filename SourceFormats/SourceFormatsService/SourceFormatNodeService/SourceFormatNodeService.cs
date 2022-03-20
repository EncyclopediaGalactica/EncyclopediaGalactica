namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.SourceFormatNodeService;

using Dtos;
using Entities;
using FluentValidation;
using Guards;
using Interfaces;
using Mappers.Interfaces;
using Repository.Interfaces;
using SourceFormatsCacheService.Interfaces;

public partial class SourceFormatNodeService : ISourceFormatNodeService
{
    private const string SourceFormatNodesListKey = "SourceFormatNodesList";
    private readonly IGuardService _guard;
    private readonly ISourceFormatMappers _sourceFormatMappers;
    private readonly ISourceFormatNodeCacheService _sourceFormatNodeCacheService;
    private readonly IValidator<SourceFormatNodeDto> _sourceFormatNodeDtoValidator;
    private readonly ISourceFormatsNodeRepository _sourceFormatsNodeRepository;
    private int _cacheExpiresInMinutes = 60;

    public SourceFormatNodeService(
        IValidator<SourceFormatNodeDto> sourceFormatNodeDtoValidator,
        IGuardService guardService,
        ISourceFormatMappers sourceFormatMappers,
        ISourceFormatsNodeRepository sourceFormatsNodeRepository,
        ISourceFormatNodeCacheService sourceFormatNodeCacheService)
    {
        _sourceFormatNodeDtoValidator = sourceFormatNodeDtoValidator ??
                                        throw new ArgumentNullException(nameof(sourceFormatNodeDtoValidator));
        _guard = guardService ?? throw new ArgumentNullException(nameof(guardService));
        _sourceFormatMappers = sourceFormatMappers ?? throw new ArgumentNullException(nameof(sourceFormatMappers));
        _sourceFormatsNodeRepository = sourceFormatsNodeRepository ??
                                       throw new ArgumentNullException(nameof(sourceFormatsNodeRepository));
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