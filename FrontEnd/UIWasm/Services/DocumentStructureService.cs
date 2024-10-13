namespace UIWasm.Services;

using EncyclopediaGalactica.Common.Contracts;

public class DocumentStructureService : IDocumentStructureService
{
    private ICollection<DocumentStructureResult> _storage = new List<DocumentStructureResult>
    {
        new DocumentStructureResult
        {
            Id = 1,
            Name = "HTML5 Page Structure",
            Description = "A HTML5 Page structure.",
        },
        new DocumentStructureResult
        {
            Id = 2,
            Name = "Gutenberg.org TXT file structure.",
            Description = "The structure of the TXT files used at Gutenberg.org.",
        },
        new DocumentStructureResult
        {
            Id = 3,
            Name = "Atom2 RSS XML Structure",
            Description = "Atom2 RSS XML Structure",
        },
    };

    public async Task<ICollection<DocumentStructureResult>> GetAllAsync()
    {
        return _storage;
    }
}