namespace UIWasm.Services;

public interface IDocumentTypeService
{
    Task<ICollection<DocumentTypeResult>> GetAll();
}