namespace EncyclopediaGalactica.SourceFormats.Mappers.Interfaces;

using Dtos;
using Entities;

public interface ISourceFormatNodeMappers
{
    SourceFormatNode MapSourceFormatNodeDtoToSourceFormatNode(SourceFormatNodeDto dto);
    SourceFormatNodeDto MapSourceFormatNodeToSourceFormatNodeDto(SourceFormatNode node);
}