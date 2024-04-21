using EncyclopediaGalactica.BusinessLogic.Contracts;

namespace UIWasm.Services;

public class DocumentTypeService : IDocumentTypeService
{
    private ICollection<DocumentTypeResult> _storage = new List<DocumentTypeResult>
    {
        new DocumentTypeResult
        {
            Id = 1,
            Name = "Wikipedia HTML Document",
            Description = "Content from Wikipedia."
        },
        new DocumentTypeResult
        {
            Id = 2,
            Name = "HTML 4.0 Document",
            Description = "HTML v4.0 document type"
        },
        new DocumentTypeResult
        {
            Id = 3,
            Name = "XML Document",
            Description = "XML Document"
        },
        new DocumentTypeResult
        {
            Id = 4,
            Name = "Atom XML Document",
            Description = "Atom XML Document"
        },
        new DocumentTypeResult
        {
            Id = 5,
            Name = "Gutenberg.org Text File",
            Description = "Gutenberg.org Text File"
        },
        new DocumentTypeResult
        {
            Id = 6,
            Name = "HTML5 Document",
            Description = "HTML5 Document"
        },
        new DocumentTypeResult
        {
            Id = 6,
            Name = "Epub format",
            Description = "Epub format"
        },
    };

    public async Task<ICollection<DocumentTypeResult>> GetAllAsync()
    {
        return _storage;
    }
}