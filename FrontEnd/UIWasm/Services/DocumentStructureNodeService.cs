#region

#endregion

namespace UIWasm.Services;

using EncyclopediaGalactica.Common.Contracts;

public class DocumentStructureNodeService : IDocumentStructureNodeService
{
    private readonly List<DocumentStructureNodeResult> _storage = new List<DocumentStructureNodeResult>
    {
        new DocumentStructureNodeResult
        {
            Id = 1,
            DocumentId = 1,
            ParentId = 0,
            Name = "Root node of HTML document",
            Description = "Description",
            HasChildren = true
        },
        // Level 1
        new DocumentStructureNodeResult
        {
            Id = 2,
            DocumentId = 1,
            ParentId = 1,
            Name = "Level 1 - 1",
            Description = "Description",
            HasChildren = true
        },
        new DocumentStructureNodeResult
        {
            Id = 3,
            DocumentId = 1,
            ParentId = 1,
            Name = "Level 1 - 2",
            Description = "Description",
            HasChildren = true
        },
        new DocumentStructureNodeResult
        {
            Id = 4,
            DocumentId = 1,
            ParentId = 1,
            Name = "Level 1 - 3",
            Description = "Description",
            HasChildren = true
        },
        // Level 1 - 1 - x
        new DocumentStructureNodeResult
        {
            Id = 5,
            DocumentId = 1,
            ParentId = 2,
            Name = "Level 1 - 1 - 1",
            Description = "Description"
        },
        new DocumentStructureNodeResult
        {
            Id = 6,
            DocumentId = 1,
            ParentId = 2,
            Name = "Level 1 - 1 - 2",
            Description = "Description"
        },
        new DocumentStructureNodeResult
        {
            Id = 7,
            DocumentId = 1,
            ParentId = 2,
            Name = "Level 1 - 1 - 3",
            Description = "Description"
        },
        // Level 1 - 2 - x
        new DocumentStructureNodeResult
        {
            Id = 8,
            DocumentId = 1,
            ParentId = 3,
            Name = "Level 1 - 2 - 1",
            Description = "Description"
        },
        new DocumentStructureNodeResult
        {
            Id = 9,
            DocumentId = 1,
            ParentId = 3,
            Name = "Level 1 - 2 - 2",
            Description = "Description"
        },
        new DocumentStructureNodeResult
        {
            Id = 10,
            DocumentId = 1,
            ParentId = 3,
            Name = "Level 1 - 2 - 3",
            Description = "Description"
        },
        // Level 1 - 3 - x
        new DocumentStructureNodeResult
        {
            Id = 11,
            DocumentId = 1,
            ParentId = 4,
            Name = "Level 1 - 3 - 1",
            Description = "Description"
        },
        new DocumentStructureNodeResult
        {
            Id = 12,
            DocumentId = 1,
            ParentId = 4,
            Name = "Level 1 - 3 - 2",
            Description = "Description"
        },
        new DocumentStructureNodeResult
        {
            Id = 13,
            DocumentId = 1,
            ParentId = 4,
            Name = "Level 1 - 3 - 3",
            Description = "Description"
        },
    };

    public ICollection<DocumentStructureNodeResult> GetChildrenOfANode(
        long nodeId,
        CancellationToken cancellationToken = default)
    {
        return _storage.Where(i => i.ParentId == nodeId).ToList();
    }
}