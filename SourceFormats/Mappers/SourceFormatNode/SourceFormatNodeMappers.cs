namespace EncyclopediaGalactica.SourceFormats.Mappers.SourceFormatNode;

using Dtos;
using Entities;
using Exceptions.SourceFormatNode;
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

    /// <inheritdoc />
    public SourceFormatNodeDto MapSourceFormatNodeToSourceFormatNodeDtoInFlatFashion(SourceFormatNode node)
    {
        if (node is null)
            throw new ArgumentNullException(nameof(node));

        SourceFormatNodeDto result = new SourceFormatNodeDto();
        result.Id = node.Id;
        result.Name = node.Name;

        return result;
    }

    /// <inheritdoc />
    public List<SourceFormatNodeDto> MapSourceFormatNodesToSourceFormatNodeDtosInFlatFashion(
        List<SourceFormatNode> sourceFormatNodes)
    {
        try
        {
            if (sourceFormatNodes is null)
                throw new ArgumentNullException(nameof(sourceFormatNodes));

            List<SourceFormatNodeDto> result = new List<SourceFormatNodeDto>();
            foreach (SourceFormatNode sourceFormatNode in sourceFormatNodes)
            {
                SourceFormatNodeDto elem = MapSourceFormatNodeToSourceFormatNodeDtoInFlatFashion(sourceFormatNode);
                result.Add(elem);
            }

            return result;
        }
        catch (Exception e)
        {
            string msg = $"Error happened while executing {nameof(SourceFormatNodeMappers)}" +
                         $".{nameof(MapSourceFormatNodesToSourceFormatNodeDtosInFlatFashion)}. For further " +
                         "information see inner exception.";
            throw new SourceFormatNodeMapperException(msg, e);
        }
    }
}