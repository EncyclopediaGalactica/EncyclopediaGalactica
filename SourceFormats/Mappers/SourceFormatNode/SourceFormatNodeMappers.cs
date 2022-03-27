namespace EncyclopediaGalactica.SourceFormats.Mappers.SourceFormatNode;

using Dtos;
using Entities;
using Interfaces;

public class SourceFormatNodeMappers : ISourceFormatNodeMappers
{
    public SourceFormatNode MapSourceFormatNodeDtoToSourceFormatNode(SourceFormatNodeDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));

        SourceFormatNode result = new SourceFormatNode();
        result.Id = dto.Id;
        result.Name = dto.Name;

        return result;
    }

    public SourceFormatNodeDto MapSourceFormatNodeToSourceFormatNodeDto(SourceFormatNode node)
    {
        if (node is null)
            throw new ArgumentNullException(nameof(node));

        SourceFormatNodeDto result = new SourceFormatNodeDto();
        result.Id = node.Id;
        result.Name = node.Name;

        return result;
    }
}