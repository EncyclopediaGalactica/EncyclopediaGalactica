namespace EncyclopediaGalactica.Services.Document.Mappers.Structure;

using Contracts.Input;
using Entities;

public partial class StructureNodeMappers
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
}