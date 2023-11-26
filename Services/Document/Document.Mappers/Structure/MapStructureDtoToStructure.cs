namespace EncyclopediaGalactica.Services.Document.Mappers.Structure;

using Contracts.Input;
using Entities;

public partial class StructureMappers
{
    /// <inheritdoc />
    public Structure MapStructureDtoToStructure(StructureInput structureInput)
    {
        return new Structure
        {
            Id = structureInput.Id,
            ParentId = structureInput.ParentId,
            Children = MapStructureDtosToStructures(structureInput.Children)
        };
    }
}