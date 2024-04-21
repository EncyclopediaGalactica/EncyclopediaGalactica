using EncyclopediaGalactica.BusinessLogic.Contracts;

namespace UIWasm.Services;

public class DocumentStructureService : IDocumentStructureService
{
    private ICollection<DocumentResult> _storage = new List<DocumentResult>
    {
        new DocumentResult
        {
            Id = 1,
            Name = "HTML page",
            Description = "HTML page",
            StructureNodes = new List<StructureNodeResult>()
        },
        new DocumentResult
        {
            Id = 2,
            Name = "TXT Book",
            Description = "TXT Book",
            StructureNodes = new List<StructureNodeResult>()
        },
        new DocumentResult
        {
            Id = 3,
            Name = "RSS",
            Description = "RSS",
            StructureNodes = new List<StructureNodeResult>()
        },
    };

    public async Task<ICollection<DocumentResult>> GetAllAsync()
    {
        return _storage;
    }
}