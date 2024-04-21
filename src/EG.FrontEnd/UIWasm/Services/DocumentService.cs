using EncyclopediaGalactica.BusinessLogic.Contracts;

namespace UIWasm.Services;

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
}