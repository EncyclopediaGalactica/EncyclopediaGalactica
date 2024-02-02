namespace EncyclopediaGalactica.Services.Document.Mappers.SourceFormatNode;

using Contracts.Input;
using Entities;
using Interfaces;

/// <inheritdoc />
public class SourceFormatNodeMappers : ISourceFormatNodeMappers
{
    /// <inheritdoc />
    public SourceFormatNode MapSourceFormatNodeDtoToSourceFormatNode(SourceFormatNodeInput input)
    {
        ArgumentNullException.ThrowIfNull(input);

        SourceFormatNode result = new SourceFormatNode();
        result.Id = input.Id;
        result.Name = input.Name;
        result.IsRootNode = input.IsRootNode;

        return result;
    }

    /// <inheritdoc />
    public SourceFormatNodeInput MapSourceFormatNodeToSourceFormatNodeDtoInFlatFashion(SourceFormatNode node)
    {
        ArgumentNullException.ThrowIfNull(node);

        SourceFormatNodeInput result = new SourceFormatNodeInput();
        result.Id = node.Id;
        result.Name = node.Name;
        result.IsRootNode = node.IsRootNode;

        return result;
    }

    /// <inheritdoc />
    public List<SourceFormatNodeInput> MapSourceFormatNodesToSourceFormatNodeDtosInFlatFashion(
        List<SourceFormatNode> sourceFormatNodes)
    {
        ArgumentNullException.ThrowIfNull(sourceFormatNodes);

        List<SourceFormatNodeInput> result = new List<SourceFormatNodeInput>();
        foreach (SourceFormatNode sourceFormatNode in sourceFormatNodes)
        {
            SourceFormatNodeInput
                elem = MapSourceFormatNodeToSourceFormatNodeDtoInFlatFashion(sourceFormatNode);
            result.Add(elem);
        }

        return result;
    }

    /// <inheritdoc />
    public SourceFormatNodeInput MapSourceFormatNodeToSourceFormatNodeDto(SourceFormatNode sourceFormatNode)
    {
        ArgumentNullException.ThrowIfNull(sourceFormatNode);

        SourceFormatNodeInput input = new SourceFormatNodeInput();
        input.Id = sourceFormatNode.Id;
        input.Name = sourceFormatNode.Name;
        input.IsRootNode = sourceFormatNode.IsRootNode;
        input.RootNodeId = sourceFormatNode.RootNodeId;
        input.ParentNodeId = sourceFormatNode.ParentNodeId;

        return input;
    }
}