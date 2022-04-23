namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.SourceFormatNodeService;

using System.Net;
using Dtos;
using Entities;
using FluentValidation;
using Interfaces;
using Mappers.Interfaces;
using Microsoft.Extensions.Logging;
using Repository.Interfaces;
using Sdk.Models.SourceFormatNode;
using SourceFormatsCacheService.Interfaces;
using Utils.GuardsService;

public partial class SourceFormatNodeService : ISourceFormatNodeService
{
    private const string SourceFormatNodesListKey = "SourceFormatNodesList";
    private readonly IGuardsService _guards;
    private readonly ISourceFormatMappers _sourceFormatMappers;
    private readonly ISourceFormatNodeCacheService _sourceFormatNodeCacheService;
    private readonly IValidator<SourceFormatNodeDto> _sourceFormatNodeDtoValidator;
    private readonly ISourceFormatNodeRepository _sourceFormatNodeRepository;
    private int _cacheExpiresInMinutes = 60;
    private readonly ILogger _logger;

    public SourceFormatNodeService(
        IValidator<SourceFormatNodeDto> sourceFormatNodeDtoValidator,
        IGuardsService guardsService,
        ISourceFormatMappers sourceFormatMappers,
        ISourceFormatNodeRepository sourceFormatNodeRepository,
        ISourceFormatNodeCacheService sourceFormatNodeCacheService,
        ILogger<SourceFormatNodeService> logger)
    {
        ArgumentNullException.ThrowIfNull(sourceFormatNodeDtoValidator);
        ArgumentNullException.ThrowIfNull(guardsService);
        ArgumentNullException.ThrowIfNull(sourceFormatMappers);
        ArgumentNullException.ThrowIfNull(sourceFormatNodeRepository);
        ArgumentNullException.ThrowIfNull(sourceFormatNodeCacheService);
        ArgumentNullException.ThrowIfNull(logger);

        _sourceFormatNodeDtoValidator = sourceFormatNodeDtoValidator;
        _guards = guardsService;
        _sourceFormatMappers = sourceFormatMappers;
        _sourceFormatNodeRepository = sourceFormatNodeRepository;
        _sourceFormatNodeCacheService = sourceFormatNodeCacheService;
        _logger = logger;
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

    private SourceFormatNodeAddResponseModel PrepareSuccessResponseModel(SourceFormatNodeDto dto)
    {
        SourceFormatNodeAddResponseModel responseModel = new SourceFormatNodeAddResponseModel.Builder()
            .SetResult(dto)
            .SetHttpStatusCode(HttpStatusCode.Created)
            .SetOperationSuccessful()
            .Build();
        return responseModel;
    }
}