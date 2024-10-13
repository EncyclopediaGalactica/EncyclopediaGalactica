namespace UIWasm.Services;

using EncyclopediaGalactica.Common.Contracts;

public class RelationService : IRelationService
{
    private ICollection<RelationResult> _storage = new List<RelationResult>
    {
        new RelationResult
        {
            Id = 1,
            LeftDocument = new DocumentResult
            {
                Id = 1,
                Name = "Karman Todor Wikipedia page",
                Description = "Karman Todor Wikipedia page"
            },
            LeftDocumentId = 1,
            RelationType = new RelationTypeResult
            {
                Id = 3,
                Name = "RelatesTo",
                Description = "Describes a connection between two related document."
            },
            RelationTypeId = 3,
            RightDocument = new DocumentResult
            {
                Id = 2,
                Name = "NeoVim Editor Wikipedia page",
                Description = "NeoVim Editor Wikipedia Page"
            },
            RightDocumentId = 2
        },
        new RelationResult
        {
            Id = 1,
            LeftDocument = new DocumentResult
            {
                Id = 1,
                Name = "Karman Todor Wikipedia page",
                Description = "Karman Todor Wikipedia page"
            },
            LeftDocumentId = 1,
            RelationType = new RelationTypeResult
            {
                Id = 3,
                Name = "RelatesTo",
                Description = "Describes a connection between two related document."
            },
            RelationTypeId = 3,
            RightDocument = new DocumentResult
            {
                Id = 3,
                Name = "Jules Verne book from Gutenberg.org",
                Description = "Jules Verne book from Gutenberg.org"
            },
            RightDocumentId = 3
        },
    };

    public async Task<ICollection<RelationResult>> GetAllAsync()
    {
        return _storage;
    }
}