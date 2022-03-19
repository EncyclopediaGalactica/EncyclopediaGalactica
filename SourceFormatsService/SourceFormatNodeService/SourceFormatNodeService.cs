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
    private readonly IValidator<SourceFormatNodeDto> _sourceFormatNodeDtoValidator;
    private readonly ISourceFormatMappers _sourceFormatMappers;
    private readonly ISourceFormatsNodeRepository _sourceFormatsNodeRepository;
    private readonly ISourceFormatNodeCacheService _sourceFormatNodeCacheService;
    private int _cacheExpiresInMinutes = 60;

    private const string SourceFormatNodesListKey = "SourceFormatNodesList";

    public SourceFormatNodeService(
        IValidator<SourceFormatNodeDto> sourceFormatNodeDtoValidator,
        ISourceFormatMappers sourceFormatMappers,
        ISourceFormatsNodeRepository sourceFormatsNodeRepository,
        ISourceFormatNodeCacheService sourceFormatNodeCacheService)
    {
        Guard.NotNull(sourceFormatNodeDtoValidator);
        Guard.NotNull(sourceFormatMappers);
        Guard.NotNull(sourceFormatsNodeRepository);
        Guard.NotNull(sourceFormatNodeCacheService);

        _sourceFormatNodeDtoValidator = sourceFormatNodeDtoValidator;
        _sourceFormatMappers = sourceFormatMappers;
        _sourceFormatsNodeRepository = sourceFormatsNodeRepository;
        _sourceFormatNodeCacheService = sourceFormatNodeCacheService;
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