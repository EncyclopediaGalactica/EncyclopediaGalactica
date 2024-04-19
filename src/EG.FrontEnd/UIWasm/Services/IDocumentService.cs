namespace UIWasm.Services;

using EncyclopediaGalactica.BusinessLogic.Contracts;

public interface IDocumentService
{
    Task<ICollection<DocumentResult>> GetAllAsync();
}