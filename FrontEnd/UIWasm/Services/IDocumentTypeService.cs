namespace UIWasm.Services;

using EncyclopediaGalactica.Common.Contracts;

public interface IDocumentTypeService
{
    Task<ICollection<DocumentTypeResult>> GetAllAsync();
}