#region

#endregion

namespace UIWasm.Services;

using EncyclopediaGalactica.Common.Contracts;

public interface IDocumentStructureNodeService
{
    ICollection<DocumentStructureNodeResult> GetChildrenOfANode(
        long nodeId,
        CancellationToken cancellationToken = default);
}