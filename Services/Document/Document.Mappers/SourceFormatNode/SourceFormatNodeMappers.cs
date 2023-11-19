namespace EncyclopediaGalactica.Services.Document.Mappers.SourceFormatNode;

using Contracts.Input;
using Entities;
using Interfaces;

/// <inheritdoc />
public class SourceFormatNodeMappers : ISourceFormatNodeMappers
{
    /// <inheritdoc />
    public SourceFormatNode MapSourceFormatNodeDtoToSourceFormatNode(SourceFormatNodeInputContract inputContract)
    {
        ArgumentNullException.ThrowIfNull(inputContract);

        SourceFormatNode result = new SourceFormatNode();
        result.Id = inputContract.Id;
        result.Name = inputContract.Name;
        result.IsRootNode = inputContract.IsRootNode;

        return result;
    }

    /// <inheritdoc />
    public SourceFormatNodeInputContract MapSourceFormatNodeToSourceFormatNodeDtoInFlatFashion(SourceFormatNode node)
    {
        ArgumentNullException.ThrowIfNull(node);

        SourceFormatNodeInputContract result = new SourceFormatNodeInputContract();
        result.Id = node.Id;
        result.Name = node.Name;
        result.IsRootNode = node.IsRootNode;

        return result;
    }

    /// <inheritdoc />
    public List<SourceFormatNodeInputContract> MapSourceFormatNodesToSourceFormatNodeDtosInFlatFashion(
        List<SourceFormatNode> sourceFormatNodes)
    {
        ArgumentNullException.ThrowIfNull(sourceFormatNodes);

        List<SourceFormatNodeInputContract> result = new List<SourceFormatNodeInputContract>();
        foreach (SourceFormatNode sourceFormatNode in sourceFormatNodes)
        {
            SourceFormatNodeInputContract
                elem = MapSourceFormatNodeToSourceFormatNodeDtoInFlatFashion(sourceFormatNode);
            result.Add(elem);
        }

        return result;
    }

    /// <inheritdoc />
    public SourceFormatNodeInputContract MapSourceFormatNodeToSourceFormatNodeDto(SourceFormatNode sourceFormatNode)
    {
        ArgumentNullException.ThrowIfNull(sourceFormatNode);

        SourceFormatNodeInputContract inputContract = new SourceFormatNodeInputContract();
        inputContract.Id = sourceFormatNode.Id;
        inputContract.Name = sourceFormatNode.Name;
        inputContract.IsRootNode = sourceFormatNode.IsRootNode;
        inputContract.RootNodeId = sourceFormatNode.RootNodeId;
        inputContract.ParentNodeId = sourceFormatNode.ParentNodeId;

        return inputContract;
    }
}