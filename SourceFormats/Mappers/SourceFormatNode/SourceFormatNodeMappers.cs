namespace EncyclopediaGalactica.SourceFormats.Mappers.SourceFormatNode;

using Dtos;
using Entities;
using Interfaces;

public class SourceFormatNodeMappers : ISourceFormatNodeMappers
{
    public SourceFormatNode MapSourceFormatNodeDtoToSourceFormatNode(SourceFormatNodeDto dto)
    {
        try
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            SourceFormatNode result = new SourceFormatNode();
            result.Id = dto.Id;
            result.Name = dto.Name;
            result.IsRootNode = dto.IsRootNode;

            return result;
        }
        catch (Exception e)
        {
            // logging comes here
            throw;
        }
    }

    /// <inheritdoc />
    public SourceFormatNodeDto MapSourceFormatNodeToSourceFormatNodeDtoInFlatFashion(SourceFormatNode node)
    {
        try
        {
            if (node is null)
                throw new ArgumentNullException(nameof(node));

            SourceFormatNodeDto result = new SourceFormatNodeDto();
            result.Id = node.Id;
            result.Name = node.Name;
            result.IsRootNode = node.IsRootNode;

            return result;
        }
        catch (Exception e)
        {
            // logging comes here
            throw;
        }
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
            // logging comes here
            throw;
        }
    }
}