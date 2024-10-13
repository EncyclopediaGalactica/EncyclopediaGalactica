#region

#endregion

namespace UIWasm.Services;

using EncyclopediaGalactica.Common.Contracts;

public interface IDocumentService
{
    Task<ICollection<DocumentResult>> GetAllAsync();

    Task<DocumentResult> GetById(long id, CancellationToken cancellationToken = default);
}