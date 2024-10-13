namespace UIWasm.Services;

using EncyclopediaGalactica.Common.Contracts;

public class FiletypeService : IFiletypeService
{
    private ICollection<FiletypeResult> _storage = new List<FiletypeResult>
    {
        new FiletypeResult
        {
            Id = 1,
            Name = "TXT file",
            Description = "TXT file",
            FileExtension = ".txt"
        },
        new FiletypeResult
        {
            Id = 2,
            Name = "Epub file",
            Description = "Epub file",
            FileExtension = ".epub"
        },
        new FiletypeResult
        {
            Id = 3,
            Name = "HTML file",
            Description = "HTML file",
            FileExtension = ".html"
        },
        new FiletypeResult
        {
            Id = 4,
            Name = "DOCX file",
            Description = "DOCX file",
            FileExtension = ".docx"
        },
        new FiletypeResult
        {
            Id = 5,
            Name = "ODT file",
            Description = "ODT file",
            FileExtension = ".odt"
        },
    };

    public async Task<ICollection<FiletypeResult>> GetAllAsync()
    {
        return _storage;
    }
}