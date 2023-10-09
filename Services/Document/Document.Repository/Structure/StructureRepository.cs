namespace EncyclopediaGalactica.Services.Document.Repository.Structure;

using Entities;
using Interfaces;

public class StructureRepository : IStructureRepository
{
    public async Task<Structure> AddNewAsync(Structure structure, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}