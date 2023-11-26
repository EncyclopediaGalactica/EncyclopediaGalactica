namespace EncyclopediaGalactica.Services.Document.Mappers.Structure;

using Contracts.Input;
using Entities;

public partial class StructureMappers
{
    /// <inheritdoc />
    public StructureInput MapStructureToStructureDto(Structure s)
    {
        return new StructureInput
        {
            Id = s.Id,
            ParentId = s.ParentId,
            Children = MapStructuresToStructureDtos(s.Children)
        };
    }
}