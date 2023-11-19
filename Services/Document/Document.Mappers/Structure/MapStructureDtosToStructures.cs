namespace EncyclopediaGalactica.Services.Document.Mappers.Structure;

using Contracts.Input;
using Entities;

public partial class StructureMappers
{
    /// <inheritdoc />
    public List<Structure> MapStructureDtosToStructures(List<StructureInputContract> structureDtos)
    {
        List<Structure> result = new List<Structure>();
        if (structureDtos.Any())
        {
            foreach (StructureInputContract structureDto in structureDtos)
            {
                result.Add(MapStructureDtoToStructure(structureDto));
            }
        }

        return result;
    }
}