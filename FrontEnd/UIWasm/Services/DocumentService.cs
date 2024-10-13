#region

#endregion

namespace UIWasm.Services;

using EncyclopediaGalactica.Common.Contracts;

public class DocumentService : IDocumentService
{
    private ICollection<DocumentResult> _storage = new List<DocumentResult>
    {
        new DocumentResult
        {
            Id = 1,
            Name = "Karman Todor Wikipedia page",
            Description = "Karman Todor Wikipedia page"
        },
        new DocumentResult
        {
            Id = 2,
            Name = "NeoVim Editor Wikipedia page",
            Description = "NeoVim Editor Wikipedia Page"
        },
        new DocumentResult
        {
            Id = 3,
            Name = "Jules Verne book from Gutenberg.org",
            Description = "Jules Verne book from Gutenberg.org"
        }
    };

    public async Task<ICollection<DocumentResult>> GetAllAsync()
    {
        return _storage;
    }

    public async Task<DocumentResult> GetById(long id, CancellationToken cancellationToken = default)
    {
        return new DocumentResult
        {
            Id = 1,
            Name = "Karman Todor Wikipedia page",
            Description = "Karman Todor Wikipedia page",
            DocumentStructure = new DocumentStructureResult
            {
                Id = 1,
                Name = "HTML5 Page Structure",
                Description = "A HTML5 Page structure.",
                StructureNode = new DocumentStructureNodeResult
                {
                    Id = 1,
                    DocumentId = 1,
                    Name = "HTML5 page root node",
                    Description = "HTML5 page root node to describe the whole",
                    ParentId = 0,
                    HasChildren = true
                }
            },
        };
    }
}