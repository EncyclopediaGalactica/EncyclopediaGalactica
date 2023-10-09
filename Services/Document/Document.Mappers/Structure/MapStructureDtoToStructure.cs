namespace EncyclopediaGalactica.Services.Document.Mappers.Structure;

using Dtos;
using Entities;

public partial class StructureMappers
{
    /// <inheritdoc />
    public Structure MapStructureDtoToStructure(StructureDto structureDto)
    {
        return new Structure
        {
            Id = structureDto.Id,
            ParentId = structureDto.ParentId,
            Children = MapStructureDtosToStructures(structureDto.Children)
        };
    }
}