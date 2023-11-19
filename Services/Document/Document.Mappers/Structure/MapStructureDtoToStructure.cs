namespace EncyclopediaGalactica.Services.Document.Mappers.Structure;

using Contracts.Input;
using Entities;

public partial class StructureMappers
{
    /// <inheritdoc />
    public Structure MapStructureDtoToStructure(StructureInputContract structureInputContract)
    {
        return new Structure
        {
            Id = structureInputContract.Id,
            ParentId = structureInputContract.ParentId,
            Children = MapStructureDtosToStructures(structureInputContract.Children)
        };
    }
}