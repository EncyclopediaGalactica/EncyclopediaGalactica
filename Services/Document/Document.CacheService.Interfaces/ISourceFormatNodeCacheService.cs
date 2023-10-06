namespace EncyclopediaGalactica.Services.Document.CacheService.Interfaces;

using Entities;

public interface ISourceFormatNodeCacheService
{
    Task AppendToCache(SourceFormatNode node, string key, int expiresIn);
}