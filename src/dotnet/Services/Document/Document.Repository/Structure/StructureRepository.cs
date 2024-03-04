namespace EncyclopediaGalactica.Services.Document.Repository.Structure;

using Entities;
using Interfaces;

public class StructureRepository : IStructureRepository
{
    public async Task<StructureNode> AddNewAsync(StructureNode structureNode,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}