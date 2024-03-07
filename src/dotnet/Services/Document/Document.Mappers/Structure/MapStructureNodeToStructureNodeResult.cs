namespace EncyclopediaGalactica.Services.Document.Mappers.Structure;

using Contracts.Output;
using Entities;

public partial class StructureNodeMappers
{
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