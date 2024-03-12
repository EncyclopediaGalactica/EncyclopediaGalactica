namespace EncyclopediaGalactica.BusinessLogic.Mappers;

using Contracts;
using Entities;

/// <inheritdoc />
public partial class StructureNodeMapper : IStructureNodeMapper
{
    /// <inheritdoc />
    public StructureNode MapStructureNodeInputToStructureNode(StructureNodeInput structureNodeInput)
    {
        return new StructureNode
        {
            Id = structureNodeInput.Id,
            DocumentId = structureNodeInput.DocumentId,
            IsRootNode = structureNodeInput.IsRootNode
        };
    }

    /// <inheritdoc />
    public StructureNodeResult MapStructureNodeToStructureNodeResult(StructureNode s)
    {
        return new StructureNodeResult()
        {
            Id = s.Id,
            DocumentId = s.DocumentId,
        };
    }
}