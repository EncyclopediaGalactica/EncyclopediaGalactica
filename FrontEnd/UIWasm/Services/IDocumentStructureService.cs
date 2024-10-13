#region

#endregion

namespace UIWasm.Services;

using EncyclopediaGalactica.Common.Contracts;

public interface IDocumentStructureService
{
    Task<ICollection<DocumentStructureResult>> GetAllAsync();
}