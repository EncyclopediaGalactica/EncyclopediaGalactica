namespace Sdk.Interfaces;

using EncyclopediaGalactica.SourceFormats.Dtos;

public interface ISourceFormatNodeSdk
{
    Task<SourceFormatNodeDto> AddAsync(SourceFormatNodeDto dto, CancellationToken cancellationToken = default);
}