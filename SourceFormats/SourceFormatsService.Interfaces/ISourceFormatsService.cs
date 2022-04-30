namespace EncyclopediaGalactica.SourceFormats.SourceFormatsService.Interfaces;

using SourceFormatNode;

public interface ISourceFormatsService
{
    ISourceFormatNodeService SourceFormatNode { get; }
}