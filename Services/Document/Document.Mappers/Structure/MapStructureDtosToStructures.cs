namespace EncyclopediaGalactica.Services.Document.Mappers.Structure;

using Dtos;
using Entities;

public partial class StructureMappers
{
    /// <inheritdoc />
    public List<Structure> MapStructureDtosToStructures(List<StructureDto> structureDtos)
    {
        List<Structure> result = new List<Structure>();
        if (structureDtos.Any())
        {
            foreach (StructureDto structureDto in structureDtos)
            {
                result.Add(MapStructureDtoToStructure(structureDto));
            }
        }

        return result;
    }
}