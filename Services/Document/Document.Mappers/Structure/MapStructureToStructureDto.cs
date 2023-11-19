namespace EncyclopediaGalactica.Services.Document.Mappers.Structure;

using Contracts.Input;
using Entities;

public partial class StructureMappers
{
    /// <inheritdoc />
    public StructureInputContract MapStructureToStructureDto(Structure s)
    {
        return new StructureInputContract
        {
            Id = s.Id,
            ParentId = s.ParentId,
            Children = MapStructuresToStructureDtos(s.Children)
        };
    }
}