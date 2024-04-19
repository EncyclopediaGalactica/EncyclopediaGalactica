using EncyclopediaGalactica.BusinessLogic.Contracts;

namespace UIWasm.Services;

public interface IDocumentTypeService
{
    Task<ICollection<DocumentTypeResult>> GetAll();
}