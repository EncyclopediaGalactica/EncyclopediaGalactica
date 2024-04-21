using EncyclopediaGalactica.BusinessLogic.Contracts;

namespace UIWasm.Services;

public interface IDocumentService
{
    Task<ICollection<DocumentResult>> GetAllAsync();
}