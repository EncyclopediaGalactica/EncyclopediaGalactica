namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Interfaces;

using SourceFormatNode;

/// <summary>
///     A facade interface for all SourceFormat related services.
/// </summary>
public interface ISourceFormatsService
{
    /// <summary>
    ///     Gets the SourceFormatNode service.
    /// </summary>
    ISourceFormatNodeService SourceFormatNode { get; }
}